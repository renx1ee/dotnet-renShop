using MediatR;

namespace RenStore.Application.Data.Common.Mediatr;

public class NotificationBase : INotification
{
    protected NotificationBase(string subject, string content, bool isCoupleted, string addressFrom, string addressTo)
    {
        this.Subject = subject;
        this.Content = content;
        this.IsCoupleted = isCoupleted;
        this.AddressFrom = addressFrom;
        this.AddressTo = addressTo;
    }
    
    public string Subject { get; set; }
    public string Content { get; set; }
    public bool IsCoupleted { get; set; }
    public string AddressFrom { get; set; }
    public string AddressTo { get; set; }
}