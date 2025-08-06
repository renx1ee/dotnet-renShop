namespace RenStore.Application.Dto.Order;

public class CreateOrderDto
{
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public uint Amount { get; set; } 
    public Guid ProductId { get; set; }
}