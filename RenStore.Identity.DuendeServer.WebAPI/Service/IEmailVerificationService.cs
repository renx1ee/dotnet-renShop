namespace RenStore.Identity.DuendeServer.WebAPI.Service;

public interface IEmailVerificationService
{
    string GenerateCode();
    Task StoreCodeAsync(string email, string code);
    Task<bool> VerifyCodeAsync(string email, string code);
}