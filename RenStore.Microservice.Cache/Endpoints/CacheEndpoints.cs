using RenStore.Microservice.Cache.DTOs;
using RenStore.Microservice.Cache.Services;

namespace RenStore.Microservice.Cache.Endpoints;

public static class CacheEndpoints
{
    public static IEndpointRouteBuilder MapCacheEndpoints(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/cache/");

        /*api.MapPost("/distributed", SetDistributedCache);
        api.MapGet("/distributed", GetDistributedCache);
        api.MapDelete("/distributed", DeleteDistributedCache);*/
        
        api.MapPost("/memory", SetMemoryCache);
        api.MapGet("/memory", GetMemoryCache);
        api.MapDelete("/memory", DeleteMemoryCache);
        
        return api;
    }

    /*private static async Task<IResult> SetDistributedCache(
        SetCacheRequest request,
        DistributedCacheService serviceBase)
    {
        await serviceBase.SetCacheAsync(request.key, request.value, request.seconds, CancellationToken.None);
        return Results.NoContent();
    }
    
    private static async Task<IResult> GetDistributedCache(
        GetCacheRequest request,
        DistributedCacheService serviceBase)
    {
        var result = await serviceBase.GetCacheAsync(request.key, CancellationToken.None);
        
        if (result is null) 
            return Results.NotFound();
        
        return Results.Ok(result);
    }
    
    private static async Task<IResult> DeleteDistributedCache(
        DeleteCacheRequest request,
        DistributedCacheService serviceBase)
    {
        await serviceBase.DeleteCacheAsync(request.key, CancellationToken.None);
        return Results.NoContent();
    }*/
    
    private static async Task<IResult> SetMemoryCache(
        SetCacheRequest request,
        MemoryCacheService serviceBase)
    {
        await serviceBase.SetCacheAsync(request.key, request.value, request.seconds, CancellationToken.None);
        return Results.NoContent();
    }
    
    private static async Task<IResult> GetMemoryCache(
        GetCacheRequest request,
        MemoryCacheService serviceBase)
    {
        var result = await serviceBase.GetCacheAsync(request.key, CancellationToken.None);
        
        if (result is null) 
            return Results.NotFound();
        
        return Results.Ok(result);
    }
    
    private static async Task<IResult> DeleteMemoryCache(
        DeleteCacheRequest request,
        MemoryCacheService serviceBase)
    {
        await serviceBase.DeleteCacheAsync(request.key, CancellationToken.None);
        return Results.NoContent();
    }
}