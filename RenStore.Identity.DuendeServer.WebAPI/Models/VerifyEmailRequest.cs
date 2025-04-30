namespace RenStore.Identity.DuendeServer.WebAPI.Models;

public class VerifyEmailRequest
{
    public string UserId { get; set; }
    public string Code { get; set; }
}