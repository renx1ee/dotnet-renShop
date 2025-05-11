using RenStore.Identity.DuendeServer.WebAPI.Data;
using RenStore.Identity.DuendeServer.WebAPI.DTOs;

namespace RenStore.Identity.DuendeServer.WebAPI.Service;

public class CacheSender : ICacheSender
{
    private readonly HttpClient httpClient;
    public CacheSender(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        httpClient.BaseAddress = new Uri(UrlConstants.CacheMicroserviceUrl);
    }
    public async Task SetCacheAsync(string key, string value, uint seconds)
    {
        var request = new SetCacheRequest(key, value, seconds);
        
        var response = await httpClient.PostAsJsonAsync(
            UrlConstants.DistrebutedUrl, request);
        
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
    }

    public async Task<string?> GetCacheAsync(string key)
    {
        try
        {
            var url = new Uri(new Uri(UrlConstants.DistrebutedUrl), key);
            using var response = await httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
        }
        return string.Empty;
    }
}