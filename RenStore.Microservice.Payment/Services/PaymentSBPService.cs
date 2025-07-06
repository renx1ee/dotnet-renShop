using RenStore.Microservice.Payment.Requests;

namespace RenStore.Microservice.Payment.Services;

public class PaymentSBPService
{
    public async Task<bool> PayAsync(PaymentSBPRequest request, CancellationToken cancellationToken)
    {
        return true;
    }
}