using RenStore.Microservice.Payment.DTOs;
using RenStore.Microservice.Payment.Enums;
using RenStore.Microservice.Payment.Repository;

namespace RenStore.Microservice.Payment.Services;

public class PaymentCardService
{
    private readonly IPaymentRepository paymentRepository;

    public PaymentCardService(
        IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }
    
    public async Task<bool> PayAsync(CreatePaymentDto dto)
    {
        
        
        var payment = new Models.Payment
        {
            ProductId = dto.ProductId,
            ApplicationUserId = dto.ApplicationUserId,
            OrderId = dto.OrderId,
            SellerId = dto.SellerId,
            Status = PaymentStatus.Authorized, // 
            CreatedDate = DateTime.UtcNow,
            PaymentMethod = PaymentMethodType.CreditCard,
            Discount = dto.Discount,
            TotalPrice = dto.TotalPrice
        };

        var result = await paymentRepository.CreateAsync(payment, CancellationToken.None);
        
        
        return true;
    }
}