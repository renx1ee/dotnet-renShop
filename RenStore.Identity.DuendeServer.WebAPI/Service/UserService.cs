using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Mvc;
using RenStore.Application.Data.Common.Exceptions;
using RenStore.Domain.Entities;
using RenStore.Identity.DuendeServer.WebAPI.Data;
using RenStore.Identity.DuendeServer.WebAPI.Data.IdentityConfigurations;

namespace RenStore.Identity.DuendeServer.WebAPI.Service;

public class UserService : ControllerBase
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly JwtProvider jwtProvider;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IEmailVerificationService emailVerificationService;

    public UserService(
        JwtProvider jwtProvider,
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        IEmailVerificationService emailVerificationService)
    {
        this.userManager = userManager;
        this.jwtProvider = jwtProvider;
        this.httpContextAccessor = httpContextAccessor;
        this.emailVerificationService = emailVerificationService;
    }

    public async Task<bool> Register(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        
        if (user is not null) 
            return false;
        
        user = new ApplicationUser
        {    
            UserName = email,
            Email = email,
            CreatedDate = DateTime.UtcNow,
            Role = "User"
        };
        
        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
            return true;
         
        return false;
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        
        if(user is null) return string.Empty;
        
        var result = await userManager.CheckPasswordAsync(user, password);
        
        if(!result) return string.Empty;
        
        var token = jwtProvider.GenerateToken(user);
        
        httpContextAccessor.HttpContext.Response.Cookies.Append("tasty-cookies", token);
        
        return token;
        
        /*var claims = new List<Claim>
        {
            new (ClaimTypes.Name, email),
            new (ClaimTypes.Role, "AuthUser"),
            new ("UserId", user.Id),
            new ("Role", user.Role)
        };

        var claimsIdentity = new ClaimsIdentity(
            claims: claims,
            authenticationType: "Bearer",
            nameType: ClaimTypes.Name,
            roleType: ClaimTypes.Role);

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal);*/
    }

    public async Task Logout()
    {
        httpContextAccessor.HttpContext.Response.Cookies.Delete("tasty-cookies");
    }
    
    public async Task ConfirmEmail(string email)
    {
        var user = await userManager.FindByEmailAsync(email) 
            ?? throw new Exception("");
        
        /*var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var request = httpContextAccessor.HttpContext.Request;
        
        var confirmationLink = GenerateConfirmationLink(request, user.Id, token);

        var data = new ConfirmEmailRequest
        {
            UserId = user.Id,
            To = email,
            Subject = "Confirm Your Email",
            Body = $"<a href='{confirmationLink}'>link</a>"
        };*/
        
        
        var code = emailVerificationService.GenerateCode();
        await emailVerificationService.StoreCodeAsync(user.Id, code);
        
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(UrlConstants.NOTIFICATION_MICROSERVICE_URL);
        
        var response = await httpClient.PostAsJsonAsync(
            "/api/v1/notification/email", 
            $"{code} is your verification code.");
        
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
    }

    public async Task<bool> VerifyEmail(string email, string code)
    {
        var result = await emailVerificationService.VerifyCodeAsync(email, code);

        if (result)
        {
            
        }
        
        return true;
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

    public async Task<string> ForgotPassword(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if(user is null) 
            return String.Empty;
        
        return user.PasswordHash;
    }
    
    public async Task<bool> ChangePassword(ApplicationUser user, string newPassword)
    {
        var changePasswordResult = 
            await userManager
                .ChangePasswordAsync(
                    user: user, 
                    currentPassword: user.PasswordHash!, 
                    newPassword: newPassword);

        if (changePasswordResult.Succeeded)
            return true;
        
        return false;
    }
    
    /*public string GenerateConfirmationLink(HttpRequest request, string userId, string token)
    {
        var scheme = request.Scheme;
        var host = request.Host.Value;
        
        return $"{scheme}://{host}/confirm-email?userId={userId}&token={WebUtility.UrlEncode(token)}";
    }*/
}
