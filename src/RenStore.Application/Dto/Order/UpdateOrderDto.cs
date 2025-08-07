namespace RenStore.Application.Dto.Order;

public class UpdateOrderDto
{
    public Guid Id { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ApplicationUserId { get; set; }
    public Guid ProductId { get; set; }
}