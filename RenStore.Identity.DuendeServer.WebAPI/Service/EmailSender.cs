using RenStore.Identity.DuendeServer.WebAPI.Data;
using RenStore.Identity.DuendeServer.WebAPI.DTOs;

namespace RenStore.Identity.DuendeServer.WebAPI.Service;

public class EmailSender(HttpClient httpClient, IConfiguration configuration) : IEmailSender
{
    public async Task SendEmail(string userId, string email, string value)
    {
        httpClient.BaseAddress = new Uri(UrlConstants.NotificationMicroserviceUrl);
        try
        {
            var data = new ConfirmEmailRequest
            {
                UserId = userId,
                To = email,
                Subject = configuration.GetValue<string>("ConfirmEmail:Subject")!,
                Body = value + configuration.GetValue<string>("ConfirmEmail:Body")
            };
            
            var response = await httpClient.PostAsJsonAsync(
                UrlConstants.SendEmailUrl,
                data);
            
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            // ignored
        }
    }
}