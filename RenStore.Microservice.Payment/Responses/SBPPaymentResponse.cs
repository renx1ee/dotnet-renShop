namespace RenStore.Microservice.Payment.Responses;

public class SBPPaymentResponse
{
    public string PaymentId { get; set; }
    public string PaymentUrl { get; set; }
    public string QRCode { get; set; }
    public bool IsSuccess { get; set; }
}