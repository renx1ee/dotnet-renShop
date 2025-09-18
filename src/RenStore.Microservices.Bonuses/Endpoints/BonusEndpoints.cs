namespace RenStore.Microservices.Bonuses.Endpoints;

public static class BonusEndpoints 
{
    public static IEndpointRouteBuilder MapBonusEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v{apiVersion}");

        app.MapPost("/bonus", CreateBonus);
        
        app.MapPut("/bonus", EditBonus);
        
        app.MapDelete("/bonus", DeleteBonus);
        
        app.MapGet("/bonus-by-id", GetBonusById);
        
        app.MapGet("/bonuses", GetAll);
        
        app.MapGet("/bonuses-by-seller-id", GetBySellerId);
        
        app.MapGet("/bonuses-by-product-id", GetByProductId);
        
        app.MapGet("/bonuses-by-user-id", GetByUserId);
        
        app.MapGet("/activated-bonuses-by-user-id", GetActivatedByUserId);
        
        return app;
    }

    private static async Task<IResult> CreateBonus()
    {
        return Results.Ok();
    }
    
    private static async Task<IResult> EditBonus()
    {
        return Results.Ok();
    }
    
    private static async Task<IResult> DeleteBonus()
    {
        return Results.Ok();
    }
    
    private static async Task<IResult> GetBonusById()
    {
        return Results.Ok();
    }
    
    private static async Task<IResult> GetAll()
    {
        return Results.Ok();
    }
    
    private static async Task<IResult> GetBySellerId()
    {
        return Results.Ok();
    }
    
    private static async Task<IResult> GetByProductId()
    {
        return Results.Ok();
    }
    
    private static async Task<IResult> GetByUserId()
    {
        return Results.Ok();
    }
    
    private static async Task<IResult> GetForUserId()
    {
        return Results.Ok();
    }
    
    private static async Task<IResult> GetActivatedByUserId()
    {
        return Results.Ok();
    }
}