using RenStore.Microservice.Payment.Common;
using RenStore.Microservice.Payment.Requests;
using RenStore.Microservice.Payment.Responses;

namespace RenStore.Microservice.Payment.Services;

public interface ITinkoffSBPService
{
    Task<Result<SBPPaymentResponse>> CreatePaymentAsync(PaymentSBPRequest request, CancellationToken cancellationToken);
}