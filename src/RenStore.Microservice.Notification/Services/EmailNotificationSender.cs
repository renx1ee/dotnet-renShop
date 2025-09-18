using MailKit.Net.Smtp;
using MimeKit;
using RenStore.Microservice.Notification.Common.Result;

namespace RenStore.Microservice.Notification.Services;

public class EmailNotificationSender : IEmailNotificationSender
{
    private readonly IConfiguration configuration;
    private readonly ILogger<EmailNotificationSender> logger;
    
    public EmailNotificationSender(
        IConfiguration configuration,
        ILogger<EmailNotificationSender> logger)
    { 
        this.configuration = configuration;
        this.logger = logger;
    }
    
    public async Task<Result> SendEmailAsync(string email, string subject, string message)
    {
        logger.LogInformation($"Sending email to {email} with subject {subject}.");
        
        var emailToSend = new MimeMessage();
        emailToSend.From.Add(MailboxAddress.Parse(email));
        emailToSend.To.Add(MailboxAddress.Parse(email));
        emailToSend.Subject = subject;
        emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Text)
        {
            Text = message
        };
        
        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync(
                userName: configuration["EmailFrom"], 
                password: configuration["EmailPassword"]);
            await client.SendAsync(emailToSend);
            await client.DisconnectAsync(true);
        }
        catch (SmtpCommandException ex)
        {
            logger.LogError(ex, ex.Message);
            Console.WriteLine($"SMTP Error: {ex.StatusCode} - {ex.Message}");
            
            return Result.Failure(new Error(ex.Message, "SMTP Error"));
        }
        
        logger.LogInformation($"Sent email to {email} with subject {subject}.");
        return Result.Success;
    }
}