using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Identity;
using RenStore.Domain.Entities;
using RenStore.Identity.DuendeServer.WebAPI.DTOs;
using RenStore.Identity.DuendeServer.WebAPI.Service;

namespace RenStore.Identity.DuendeServer.WebAPI.Endpoints;

public static class UserEndpoints 
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth/");
        
        group.MapPost("/register", Register);

        group.MapPost("/login", Login);

        group.MapPost("/logout", Logout).RequireAuthorization();
        
        group.MapPost("/send-code-email", ConfirmEmail);
        
        group.MapPost("/verify-code-email", VerifyEmail);

        group.MapGet("/check-confirmed-email", CheckConfirmEmail);

        group.MapPost("/forgot-password", ForgotPassword);

        group.MapPost("/change-password", ChangePassword);
        
        group.MapPost("/refresh-token", RefreshToken).RequireAuthorization();

        group.MapGet("/me", GetMyInfo);
        
        group.MapPost("/update-me", UpdateProfile);
        
        group.MapPost("/assign-role", AssignRole);
        
        group.MapPost("/remove-role", RemoveRole);
        
        group.MapGet("/user-email", GetUserByEmail);
        
        return group;
    }

    private static async Task<IResult> Register(
        RegisterUserRequest request,
        UserService userService,
        IEmailVerificationService emailVerificationService)
    {
        var result = await userService.Register(
            email: request.Email, 
            password: request.Password);

        if (result) return Results.Ok();
        
        return Results.BadRequest();
    }

    private static async Task<IResult> Login(
        LoginUserRequest request,
        UserService userService)
    {
        var result = await userService.Login(request.Email!, request.Password!);
        
        if(!result.IsNullOrEmpty())
            return Results.Ok(result);
        
        return Results.BadRequest("Email or password is incorrect.");
    }
    
    private static async Task<IResult> Logout()
    {
        return Results.Ok();
    }
    
    private static async Task<IResult> CheckConfirmEmail(
        string email,
        UserService userService)
    {
        var result = await userService.CheckEmailConfirmed(email);
        
        return Results.Ok($"{result}");
    }
    
    private static async Task<IResult> ConfirmEmail(
        string email,
        UserService userService)
    {
        await userService.ConfirmEmail(email);
        return Results.Ok();
    }
    private static async Task<IResult> VerifyEmail(
        string email,
        VerifyEmailRequest request,
        UserService userService)
    {
        await userService.ConfirmEmail(email);
        return Results.Ok();
    }

    private static async Task<IResult> ForgotPassword()
    {
        return Results.Ok();
    }
    
    private static async Task<IResult> ChangePassword(
        ChangePasswordRequest request, 
        UserService service,
        HttpContext context,
        UserManager<ApplicationUser> userManager)
    {
        var user = await userManager.GetUserAsync(context.User);
        if (user is null) 
            return Results.Unauthorized();
        
        var result = await service 
            .ChangePassword(user, request.NewPassword);
        
        return Results.Ok();
    }
    
    private static async Task<IResult> RefreshToken()
    {
        return Results.Ok();
    }

    private static async Task<IResult> GetMyInfo(
        UserManager<ApplicationUser> manager,
        HttpContext context)
    {
        var user = context.User;
        var jwtToken = context.Request.Cookies["tasty-cookies"];
        
        if (!context.User.Identity.IsAuthenticated)
            return Results.Unauthorized();
        
        return Results.Ok();
    }
    
    private static async Task<IResult> UpdateProfile()
    {
        return Results.Ok();
    }

    private static async Task<IResult> AssignRole()
    {
        return Results.Ok();
    }
    
    private static async Task<IResult> RemoveRole()
    {
        return Results.Ok();
    }

    private static async Task<IResult> GetUserByEmail()
    {
        return Results.Ok();
    }
}