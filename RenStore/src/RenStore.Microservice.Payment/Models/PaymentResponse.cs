namespace RenStore.Microservice.Payment.Models;

public class PaymentResponse
{
    public bool IsSuccess  { get; set; }
    public string TransactionId { get; set; }
    public string ErrorMessage { get; set; }
}