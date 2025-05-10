using RenStore.Identity.DuendeServer.WebAPI.Data;
using RenStore.Identity.DuendeServer.WebAPI.DTOs;

namespace RenStore.Identity.DuendeServer.WebAPI.Service;

public class CacheSender : ICacheSender
{
    public async Task SetCacheAsync(string key, string value, uint seconds)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(UrlConstants.CacheMicroserviceUrl);

        var request = new SetCacheRequest(key, value, seconds);
        
        var response = await httpClient.PostAsJsonAsync(
            "/api/cache/distributed", request);
        
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
    }

    public async Task<string?> GetCacheAsync(string key)
    {
        return "";
    }
}