using RenStore.Domain.Enums;

namespace RenStore.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public uint Amount { get; set; } 
    public int ZipCode { get; set; }
    public DeliveryStatus Status { get; set; }
    public decimal OrderTotal { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ApplicationUserId { get; set; }
    public Guid ProductId { get; set; }
}