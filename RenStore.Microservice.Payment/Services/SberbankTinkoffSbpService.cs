using RenStore.Microservice.Payment.Common;
using RenStore.Microservice.Payment.Requests;
using RenStore.Microservice.Payment.Responses;

namespace RenStore.Microservice.Payment.Services;

public class SberbankTinkoffSbpService 
{
    private readonly IHttpClientFactory clientFactory;
    private readonly string apiKey;
    
    public SberbankTinkoffSbpService(
        IHttpClientFactory clientFactory,
        IConfiguration configuration)
    {
        this.clientFactory = clientFactory;
        this.apiKey = configuration.GetValue<string>("SberApiKey")!;
    }

    public Task<Result<SBPPaymentResponse>> CreatePaymentAsync(PaymentSBPRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}