using RenStore.Microservice.Payment.Repository;
using RenStore.Microservice.Payment.Requests;
using RenStore.Microservice.Payment.Services;

namespace RenStore.Microservice.Payment.Endpoints;

public static class PaymentEndpoints
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/app/v1/payment");

        app.MapPost("/card", PayCard);
        
        app.MapPost("/sbp", PaySbp);
        
        app.MapPost("/cancel", CancelPayment);
        
        return group;
    }

    private static async Task<IResult> PayCard(
        PaymentCardService cardService,
        CancellationToken cancellationToken)
    {
        return Results.Ok();
    }
    
    private static async Task<IResult> PaySbp(
        PaymentSBPRequest request,
        PaymentSBPService paymentSbpService,
        CancellationToken cancellationToken)
    {
        var result = 
            await paymentSbpService.PayAsync(
                request: request,
                cancellationToken: cancellationToken);
        
        if(result.IsSuccess) 
            return Results.Accepted();
        
        return Results.BadRequest();
    }
    
    private static async Task<IResult> CancelPayment(
        IPaymentRepository paymentRepository,
        CancellationToken cancellationToken)
    {
        return Results.Ok();
    }
}