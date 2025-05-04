namespace RenStore.Identity.DuendeServer.WebAPI.DTOs;

public class VerifyEmailRequest
{
    public string UserId { get; set; }
    public string Code { get; set; }
}