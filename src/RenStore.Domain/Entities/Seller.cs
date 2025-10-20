namespace RenStore.Domain.Entities;

public class Seller
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public bool IsBlocked { get; set; } = false;
    /*public Guid ApplicationUserId { get; set; }
    public ApplicationUser? User { get; set; }*/
    /*public IList<Product> Products { get; set; } = [];
    public IList<ProductAnswer> ProductAnswers { get; set; } = [];
    public IList<SellerImage> SellerImages { get; set; } = [];
    public long SellerFooterImageId { get; set; }
    public SellerFooterImage? SellerFooterImage { get; set; }*/
}
