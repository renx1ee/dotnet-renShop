using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RenStore.Application.Data.Common.Exceptions;
using RenStore.Domain.Entities;
using RenStore.Identity.DuendeServer.WebAPI.Data.Helpers;
using RenStore.Identity.DuendeServer.WebAPI.Data.IdentityConfigurations;
using RenStore.Identity.DuendeServer.WebAPI.Models;

namespace RenStore.Identity.DuendeServer.WebAPI.Service;

public class UserService : ControllerBase
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly JwtProvider jwtProvider;
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserService(
        JwtProvider jwtProvider,
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        this.userManager = userManager;
        this.jwtProvider = jwtProvider;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> Register(string email, string password)
    {
        var userExist = await userManager.FindByEmailAsync(email);
        if (userExist is not null) 
            return false;
        
        var user = new ApplicationUser
        {    
            UserName = email,
            Email = email,
            CreatedDate = DateTime.UtcNow
        };
        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            bool emailStatus = await CheckEmailConfirmed(email);
            if (emailStatus) 
                return false;

            var loginResult = await this.Login(user.Email, user.PasswordHash!);
            if (!loginResult)
                return false;
        }
        return true;
    }

    public async Task<bool> Login(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        
        if(user == null) 
            return false;
        var result = await userManager.CheckPasswordAsync(user, password);
        
        if(result) return false;
        
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, email),
            new (ClaimTypes.Role, "AuthUser"),
            new ("UserId", user.Id)
        };
        var claimsIdentity = new ClaimsIdentity(claims, "pwd", ClaimTypes.Name, ClaimTypes.Role);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await httpContextAccessor.HttpContext!.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal);

        var token = jwtProvider.GenerateToken(user);

        httpContextAccessor.HttpContext!.Response.Cookies.Append("tasty-cookies", token);

        await httpContextAccessor.HttpContext.SignInAsync(claimsPrincipal);

        return true;
    }
    
    public async Task ConfirmEmail(string email)
    {
        var user = await userManager.FindByEmailAsync(email) 
            ?? throw new Exception("");
        
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var request = httpContextAccessor.HttpContext.Request;
        
        var confirmationLink = GenerateConfirmationLink(request, user.Id, token);

        var data = new ConfirmEmailRequest
        {
            UserId = user.Id,
            To = email,
            Subject = "Confirm Your Email",
            Body = $"<a href='{confirmationLink}'>link</a>"
        };
        
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(UrlConstants.NOTIFICATION_MICROSERVICE_URL);
        
        var response = await httpClient.PostAsJsonAsync(
            "/api/v1/notification/email", 
            data);
        
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
    }

    public async Task<bool> CheckEmailConfirmed(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is not null)
        {
            bool emailStatus = await userManager.IsEmailConfirmedAsync(user);
            if (emailStatus) return true;
        }
        return false;
    }
    // TODO: доделать подтверждение.
    public string GenerateConfirmationLink(HttpRequest request, string userId, string token)
    {
        var scheme = request.Scheme;
        var host = request.Host.Value;
        
        return $"{scheme}://{host}/confirm-email?userId={userId}&token={WebUtility.UrlEncode(token)}";
    }
}