using RenStore.Microservice.Payment.Repository;
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

    public async static Task<IResult> PayCard(
        PaymentCardService cardService)
    {
        return Results.Ok();
    }
    
    public async static Task<IResult> PaySbp(
        PaymentSbpService sbpService)
    {
        return Results.Ok();
    }
    
    public async static Task<IResult> CancelPayment(
        IPaymentRepository paymentRepository)
    {
        return Results.Ok();
    }
}