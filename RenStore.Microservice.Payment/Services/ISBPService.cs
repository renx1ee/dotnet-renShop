using RenStore.Microservice.Payment.Requests;
using RenStore.Microservice.Payment.Responses;

namespace RenStore.Microservice.Payment.Services;

public interface ISBPService
{
    Task<SBPPaymentResponse> CreatePaymentAsync(PaymentSBPRequest request, CancellationToken cancellationToken);
}