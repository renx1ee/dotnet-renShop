using RenStore.Microservice.Payment.Requests;
using RenStore.Microservice.Payment.Responses;
using RenStore.Microservice.Payment.Senders;

namespace RenStore.Microservice.Payment.Services;

public class TinkoffSBPService : ISBPService
{
    private readonly IHttpClientFactory clientFactory;
    private readonly IConfiguration configuration;
    private readonly ITinkoffSBPSender tinkoffSbpSender;
    
    public TinkoffSBPService(
        IHttpClientFactory clientFactory,
        IConfiguration configuration,
        ITinkoffSBPSender tinkoffSbpSender)
    {
        this.clientFactory = clientFactory;
        this.configuration = configuration;
        this.tinkoffSbpSender = tinkoffSbpSender;
    }
    
    public async Task<SBPPaymentResponse> CreatePaymentAsync(
        PaymentSBPRequest request, 
        CancellationToken cancellationToken)
    {
        var apiKey = configuration.GetValue<string>("tinkoffApiKey")!;

        var result = await tinkoffSbpSender.SendAsync(request, cancellationToken);

        
        

        return new SBPPaymentResponse()
        {
            PaymentId = result.PaymentId,
            PaymentUrl = result.PaymentUrl,
            QRCode = result.QRCode,
            IsSuccess = result.IsSuccess
        };

        return null;
    }
}