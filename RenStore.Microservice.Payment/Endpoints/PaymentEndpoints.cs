namespace RenStore.Microservice.Payment.Endpoints;

public static class PaymentEndpoints
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/app/v1/payment");
        
        return group;
    }
}