namespace RenStore.Identity.DuendeServer.WebAPI.Data;

public static class UrlConstants
{
    // Notification microservice url
    public const string NotificationMicroserviceUrl = "http://localhost:5062/";
    public const string SendEmailUrl = "/api/v1/notification/email";
    public const string SendSmsUrl = "/api/v1/notification/sms";
    public const string SendPushUrl = "/api/v1/notification/push";
    //Cache microservice url
    public const string CacheMicroserviceUrl = "http://localhost:5197/";
    public const string DistributedUrl = "/api/cache/distributed";
}