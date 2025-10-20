using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.EntityTypeConfigurations;

public class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder
            .HasKey(seller => seller.Id);

        builder
            .Property(seller => seller.Name)
            .HasMaxLength(50)
            .IsRequired();
        
        builder
            .HasIndex(seller => seller.Name)
            .IsUnique();
        
        builder
            .Property(seller => seller.NormalizedName)
            .HasMaxLength(50)
            .IsRequired();
        
        builder
            .HasIndex(seller => seller.NormalizedName)
            .IsUnique();
        
        builder
            .Property(seller => seller.Description)
            .HasMaxLength(500)
            .IsRequired(false);
        
        builder
            .Property(seller => seller.CreatedDate)
            .IsRequired();
        
        builder
            .Property(seller => seller.IsBlocked)
            .IsRequired();
        
        /*builder
            .Property(seller => seller.ApplicationUserId)
            .IsRequired();*/
    }
}