namespace RenStore.Microservice.Payment.Requests;

public class PaymentSBPRequest
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Phone { get; set; }
    public string BankName { get; set; }
    public string Purpose { get; set; }
}