namespace RenStore.WebApi.ViewModels;

public class OrderViewModel
{
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public uint Amount { get; set; }  
    public decimal OrderTotal { get; set; }
}