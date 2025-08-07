using RenStore.Microservice.Payment.Enums;

namespace RenStore.Microservice.Payment.Requests;

public class PaymentSBPRequest
{
    public PaymentSBPBanks Bank { get; set; }
    public Guid ProductId { get; set; }
    public string ApplicationUserId { get; set; }
    public Guid OrderId { get; set; }
    public int SellerId { get; set; }
    public uint Amount { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }
}