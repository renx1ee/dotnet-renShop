using RenStore.Identity.DuendeServer.WebAPI.Models;
using RenStore.Identity.DuendeServer.WebAPI.Service;

namespace RenStore.Identity.DuendeServer.WebAPI.Endpoints;

public static class UserEndpoints 
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth/");
        
        group.MapPost("/register", Register);

        group.MapPost("/login", Login);
        
        group.MapPost("/confirm-email", ConfirmEmail);

        group.MapGet("/check-confirm-email", CheckConfirmEmail);
        
        return group;
    }

    private static async Task<IResult> Register(
        RegisterUserRequest request,
        UserService userService)
    {
        var user = await userService.Register(request.Email, request.Password);
        if(user)
            return Results.Ok();
        
        return Results.BadRequest();
    }

    private static async Task<IResult> Login(
        LoginUserRequest request,
        UserService userService)
    {
        var result = await userService.Login(request.Email!, request.Password!);
        
        if(result)
            return Results.Ok();
        
        return Results.BadRequest("Email or password is incorrect.");
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
}