using System.ComponentModel.DataAnnotations.Schema;
using RenStore.Microservice.Payment.Enums;

namespace RenStore.Microservice.Payment.Models;

public class Payment
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ApplicationUserId { get; set; }
    public Guid OrderId { get; set; }
    public int SellerId { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public PaymentMethodType PaymentMethod { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }
}