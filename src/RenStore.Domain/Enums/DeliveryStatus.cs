namespace RenStore.Domain.Enums;

public enum DeliveryStatus
{
    AwaitingConfirmation,
    OnAssemblyByTheSeller,
    OnTheWayToTheSortingCenter,
    OnTheWay,
    OnTheWayToThePickUpPoint,
    AwaitingReceipt
}