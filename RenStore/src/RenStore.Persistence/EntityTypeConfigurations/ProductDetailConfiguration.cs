using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.EntityTypeConfigurations;

public class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetail>
{
    public void Configure(EntityTypeBuilder<ProductDetail> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .Property(detail => detail.Article)
            .HasMaxLength(9)
            .IsRequired();
        
        builder
            .Property(detail => detail.Brend)
            .HasMaxLength(25)
            .IsRequired();
        
        builder
            .Property(detail => detail.CountryOfManufacture)
            .HasMaxLength(25)
            .IsRequired(false);
        
        builder
            .Property(detail => detail.ModelFeatures)
            .HasMaxLength(25)
            .IsRequired(false);
        
        builder
            .Property(detail => detail.DecorativeElements)
            .HasMaxLength(50)
            .IsRequired(false);
        
        builder
            .Property(detail => detail.Equipment)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(detail => detail.QuantityPerPackage)
            .HasMaxLength(5)
            .IsRequired(false);
        
        builder
            .Property(detail => detail.Composition)
            .HasMaxLength(50)
            .IsRequired(false);
        
        builder
            .Property(detail => detail.Color)
            .IsRequired(false);
        
        builder
            .Property(detail => detail.TypeOfPackaging)
            .IsRequired(false);
        
        builder
            .HasOne(detail => detail.Product)
            .WithOne(product => product.ProductDetail)
            .HasForeignKey<ProductDetail>(detail => detail.ProductId);

        builder
            .HasOne(detail => detail.Product)
            .WithOne(product => product.ProductDetail)
            .HasForeignKey<ProductDetail>(detail => detail.ProductId);
    }
}

