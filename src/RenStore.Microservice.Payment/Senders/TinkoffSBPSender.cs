using RenStore.Microservice.Payment.Common;
using RenStore.Microservice.Payment.Requests;
using RenStore.Microservice.Payment.Responses;

namespace RenStore.Microservice.Payment.Senders;

public class TinkoffSBPSender : ITinkoffSBPSender
{
    private readonly ILogger<TinkoffSBPSender> logger;
    private readonly HttpClient httpClient;
    private readonly IConfiguration configuration;

    public TinkoffSBPSender(
        ILogger<TinkoffSBPSender> logger,
        HttpClient httpClient,
        IConfiguration configuration)
    {
        this.logger = logger;
        this.httpClient = httpClient;
        this.configuration = configuration;
    }
    
    public async Task<SBPPaymentResponse> SendAsync(PaymentSBPRequest payload, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Sending {nameof(SendAsync)}");
        
        try
        {
            string url = configuration.GetValue<string>("TinkoffPaymentUrl") 
                ?? throw new Exception("Url is not found.");
            
            var response = 
                await httpClient.PostAsJsonAsync(
                    requestUri: url,
                    value: payload,
                    cancellationToken: cancellationToken);
            
            var result = await response.Content.ReadFromJsonAsync<SBPPaymentResponse>();
            
            logger.LogInformation($"Sent {nameof(SendAsync)}");
            
            return result;
        }
        catch ( Exception ex)
        {
            logger.LogError($"Sending error with: {nameof(SendAsync)}");
        }

        return null;
    }
}