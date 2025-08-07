using RenStore.Microservice.Payment.Requests;
using RenStore.Microservice.Payment.Responses;

namespace RenStore.Microservice.Payment.Senders;

public interface ITinkoffSBPSender
{
    Task<SBPPaymentResponse> SendAsync(PaymentSBPRequest payload, CancellationToken cancellationToken);
}