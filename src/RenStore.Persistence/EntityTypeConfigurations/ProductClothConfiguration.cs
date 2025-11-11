using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.EntityTypeConfigurations;

public class ProductClothConfiguration : IEntityTypeConfiguration<ProductClothEntity>
{
    public void Configure(EntityTypeBuilder<ProductClothEntity> builder)
    {
        builder
            .ToTable("product_clothes");
        
        builder
            .HasKey(x => x.Id);
        
        builder
            .Property(x => x.Id)
            .HasColumnName("product_clothes_id");
        
        builder
            .Property(c => c.Gender)
            .HasColumnName("gender")
            .IsRequired(false);
        
        builder
            .Property(c => c.Season)
            .HasColumnName("season")
            .IsRequired(false);
        
        builder
            .Property(c => c.Neckline)
            .HasColumnName("neckline")
            .IsRequired(false);
        
        builder
            .Property(c => c.TheCut)
            .HasColumnName("the_cut")
            .IsRequired(false);
        
        builder
            .HasOne(c => c.Product)
            .WithOne(p => p.ProductCloth)
            .HasForeignKey<ProductClothEntity>(c => c.ProductId)
            .HasConstraintName("product_id");

        builder
            .HasMany(c => c.ClothSizes)
            .WithOne(s => s.ProductCloth)
            .HasForeignKey(s => s.ProductClothId)
            .HasConstraintName("product_clothes_id");
    }
}