using RenStore.Microservice.Payment.Enums;

namespace RenStore.Microservice.Payment.DTOs;

public class CreatePaymentDto
{
    public Guid ProductId { get; set; }
    public string ApplicationUserId { get; set; }
    public Guid OrderId { get; set; }
    public int SellerId { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public PaymentMethodType PaymentMethod { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }
}