using RenStore.Microservice.Payment.Common;
using RenStore.Microservice.Payment.Enums;
using RenStore.Microservice.Payment.Repository;
using RenStore.Microservice.Payment.Requests;
using RenStore.Microservice.Payment.Responses;
using RenStore.Microservice.Payment.Senders;

namespace RenStore.Microservice.Payment.Services;

public class TinkoffSBPService : ITinkoffSBPService
{
    private readonly IConfiguration configuration;
    private readonly ITinkoffSBPSender tinkoffSbpSender;
    private readonly IPaymentRepository paymentRepository;
    
    public TinkoffSBPService(
        IConfiguration configuration,
        ITinkoffSBPSender tinkoffSbpSender,
        IPaymentRepository paymentRepository)
    {
        this.configuration = configuration;
        this.tinkoffSbpSender = tinkoffSbpSender;
        this.paymentRepository = paymentRepository;
    }
    
    public async Task<Result<SBPPaymentResponse>> CreatePaymentAsync(
        PaymentSBPRequest request, 
        CancellationToken cancellationToken)
    {
        var apiKey = configuration.GetValue<string>("tinkoffApiKey")!;
        
        var payment = new Models.Payment()
        {
            ProductId = request.ProductId,
            ApplicationUserId = request.ApplicationUserId,
            OrderId = request.OrderId,
            SellerId = request.SellerId,
            Status = PaymentStatus.Pending,
            CreatedDate = DateTime.UtcNow,
            PaymentMethod = PaymentMethodType.Sbp,
            Discount = request.Discount,
            TotalPrice = request.TotalPrice
        };

        await paymentRepository.CreateAsync(payment, cancellationToken);

        var result = await tinkoffSbpSender.SendAsync(request, cancellationToken);

        return Result<SBPPaymentResponse>.Success(
            new SBPPaymentResponse()
            {
                PaymentId = result.PaymentId,
                PaymentUrl = result.PaymentUrl,
                QRCode = result.QRCode,
                IsSuccess = result.IsSuccess
            });
    }
}