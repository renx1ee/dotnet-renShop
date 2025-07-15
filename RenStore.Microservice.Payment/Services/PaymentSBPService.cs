using RenStore.Microservice.Payment.Common;
using RenStore.Microservice.Payment.Enums;
using RenStore.Microservice.Payment.Requests;

namespace RenStore.Microservice.Payment.Services;

public class PaymentSBPService
{
    private readonly ILogger<PaymentSBPService> logger;
    private readonly TinkoffSBPService tinkoffSbpService;
    public PaymentSBPService(
        ILogger<PaymentSBPService> logger,
        TinkoffSBPService tinkoffSbpService)
    {
        this.logger = logger;
        this.tinkoffSbpService = tinkoffSbpService;
    }
    
    public async Task<Result<bool>> PayAsync(PaymentSBPRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(PayAsync)}");
        
        switch (request.Bank)
        {
            case PaymentSBPBanks.Tinkoff:
                
                logger.LogInformation($"Handling {nameof(PaymentSBPBanks.Tinkoff)} ");
                
                var result = await tinkoffSbpService
                    .CreatePaymentAsync(
                        request: request,
                        cancellationToken: cancellationToken);
                
                return Result<bool>.Success(true);
                break;
            case PaymentSBPBanks.Sber:
                break;
            case PaymentSBPBanks.Alfa:
                break;
            default:
                return Result<bool>.Failure("The bank is not found!");
                break;
        }
        
        return Result<bool>.Success(true);
    }
}