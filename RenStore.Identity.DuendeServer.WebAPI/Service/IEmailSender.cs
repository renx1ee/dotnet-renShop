namespace RenStore.Identity.DuendeServer.WebAPI.Service;

public interface IEmailSender
{
    Task SendEmail(string userId, string email, string value);
}