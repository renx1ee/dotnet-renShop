namespace RenStore.Microservice.Payment.Models;

public class CardPayment : PaymentMethod
{
    public string CardNumber { get; set; }
    public string CardBrand { get; set; }
    public string ExpiryDate { get; set; }
}