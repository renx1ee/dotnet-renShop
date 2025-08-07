namespace RenStore.Microservice.Payment.Enums;

public enum PaymentStatus
{
    Pending,
    Authorized,
    Completed,
    Failed,
    Refunded,
    Cancelled
}