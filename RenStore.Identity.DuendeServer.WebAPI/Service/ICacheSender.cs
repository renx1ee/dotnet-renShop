namespace RenStore.Identity.DuendeServer.WebAPI.Service;

public interface ICacheSender
{
    Task SetCacheAsync(string key, string value, uint seconds);
    Task<string?> GetCacheAsync(string key);
}