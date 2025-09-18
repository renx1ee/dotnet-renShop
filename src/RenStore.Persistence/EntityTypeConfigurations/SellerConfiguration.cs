using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.EntityTypeConfigurations;

public class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.HasKey(seller => seller.Id);

        builder
            .Property(seller => seller.Name)
            .HasMaxLength(35)
            .IsRequired();
        
        builder
            .Property(seller => seller.Description)
            .HasMaxLength(500)
            .IsRequired(false);
        
        builder
            .Property(seller => seller.ImagePath)
            .HasMaxLength(200)
            .IsRequired(false);
        
        builder
            .Property(seller => seller.ImageFooterPath)
            .HasMaxLength(200)
            .IsRequired(false);
        
        builder
            .HasMany(seller => seller.Products)
            .WithOne(product => product.Seller)
            .HasForeignKey(seller => seller.SellerId);
    }
}