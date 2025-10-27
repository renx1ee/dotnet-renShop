namespace RenStore.Microservice.Payment.Models;

public class Address
{
    public Guid Id { get; set; }
    public string Country { get; set; } = string.Empty;
    public string City { get; set; }  = string.Empty;
    public string Street { get; set; }  = string.Empty;
    public string PostalCode { get; set; }  = string.Empty;
}