using RenStore.Domain.Enums;

namespace RenStore.Application.Features.Delivery.Queries.GetAllDeliveriesByProductId;

public class GetAllDeliveriesByProductIdVm
{
    public Guid Id { get; set; }
    public string UserId { get; set; } 
    public DateTime CreatedDate { get; set; }
    public DeliveryStatus Status { get; set; }
    public string Address { get; set; }

    public GetAllDeliveriesByProductIdVm(Guid id, string userId, DateTime createdDate,  DeliveryStatus status, string address)
    {
        Id = id;
        UserId = userId;
        CreatedDate = createdDate;
        Status = status;
        Address = address;
    }
}