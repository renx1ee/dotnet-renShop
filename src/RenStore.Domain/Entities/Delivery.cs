namespace RenStore.Domain.Entities;

public class Delivery
{
    public Guid Id { get; set; }
    public string UserId { get; set; } 
    public DateTime CreatedDate { get; set; }
    public DeliveryStatus Status { get; set; }
    public string Address { get; set; }
}