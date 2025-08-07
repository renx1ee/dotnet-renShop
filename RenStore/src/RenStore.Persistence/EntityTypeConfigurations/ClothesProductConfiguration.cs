using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenStore.Domain.Entities.Products;

namespace RenStore.Persistence.EntityTypeConfigurations;

public class ClothesProductConfiguration : IEntityTypeConfiguration<ClothesProduct>
{
    public void Configure(EntityTypeBuilder<ClothesProduct> builder)
    {
        builder.HasKey(clothes => clothes.Id);
        
        builder
            .Property(clothes => clothes.Neckline)
            .HasMaxLength(25)
            .IsRequired(false);
        
        builder
            .Property(clothes => clothes.TheCut)
            .HasMaxLength(25)
            .IsRequired(false);
        
        builder
            .Property(clothes => clothes.TypeOfPockets)
            .HasMaxLength(25)
            .IsRequired(false);

        builder
            .Property(clothes => clothes.Gender)
            .IsRequired();
        
        builder
            .Property(clothes => clothes.Season)
            .IsRequired();
        
        builder
            .Property(clothes => clothes.TakingCareOfThings)
            .HasMaxLength(50)
            .IsRequired(false);

        builder
            .Property(clothes => clothes.Sizes)
            .IsRequired();

        builder
            .HasOne(shoes => shoes.Product)
            .WithOne(product => product.ClothesProduct)
            .HasForeignKey<ClothesProduct>(shoes => shoes.ProductId);
    }
}