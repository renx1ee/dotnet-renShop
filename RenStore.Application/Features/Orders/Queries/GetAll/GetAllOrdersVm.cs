using RenStore.Domain.Enums;

namespace RenStore.Application.Features.Orders.Queries.GetAll;

public class GetAllOrdersVm
{
    public Guid Id { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public uint Amount { get; set; } 
    public int ZipCode { get; set; }
    public DeliveryStatus Status { get; set; }
    public decimal OrderTotal { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ApplicationUserId { get; set; }
    public Guid ProductId { get; set; }

    public GetAllOrdersVm(Guid id, 
        string address, 
        string city, 
        string country, 
        uint amount, 
        int zipCode, 
        DeliveryStatus status, 
        decimal orderTotal, 
        DateTime createdBy, 
        string applicationUserId, 
        Guid productId)
    {
        this.Id = id;
        this.Address = address;
        this.City = city;
        this.Country = country;
        this.Amount = amount;
        this.ZipCode = zipCode;
        this.Status = status;
        this.OrderTotal = orderTotal;
        this.CreatedDate = createdBy;
        this.ApplicationUserId = applicationUserId;
        this.ProductId = productId;
    }
}