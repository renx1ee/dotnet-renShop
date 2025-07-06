using RenStore.Microservice.Payment.Requests;
using RenStore.Microservice.Payment.Responses;

namespace RenStore.Microservice.Payment.Services;

public class SberbankSBPService : ISBPService
{
    private readonly IHttpClientFactory clientFactory;
    private readonly string apiKey;
    
    public SberbankSBPService(
        IHttpClientFactory clientFactory,
        IConfiguration configuration)
    {
        this.clientFactory = clientFactory;
        this.apiKey = configuration.GetValue<string>("SberApiKey")!;
    }
    
    public async Task<SBPPaymentResponse> CreatePaymentAsync(PaymentSBPRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}