namespace RenStore.Identity.DuendeServer.WebAPI.Service;

public interface IEmailVerificationService
{
    string GenerateCode();
    Task StoreCodeAsync(string userId, string code);
    Task<bool> VerifyCodeAsync(string userId, string code);
}