using Microsoft.Extensions.Caching.Memory;
using RenStore.Identity.DuendeServer.WebAPI.Data;

namespace RenStore.Identity.DuendeServer.WebAPI.Service;

public class EmailVerificationService : IEmailVerificationService
{
    private readonly IMemoryCache cache;
    private readonly TimeSpan expiration = TimeSpan.FromMinutes(5);

    public EmailVerificationService(IMemoryCache cache)
    {
        this.cache = cache;
    }
    
    public string GenerateCode()
    {
        var random = new Random();
        return random.Next(1000, 9999).ToString();
    }

    public async Task StoreCodeAsync(string userId, string code)
    {
        cache.Set($"EmailVerification_{userId}", code, expiration);
    }

    public async Task<bool> VerifyCodeAsync(string userId, string code)
    {
        if (cache.TryGetValue($"EmailVerification_{userId}", out string storedCode))
        {
            return storedCode == code;
        }
        return false;
    }
}