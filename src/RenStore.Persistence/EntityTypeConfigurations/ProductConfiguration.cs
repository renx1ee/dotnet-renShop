using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenStore.Domain.Entities;
using RenStore.Domain.Entities.Products;

namespace RenStore.Persistence.EntityTypeConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(product => product.ProductName)
            .HasMaxLength(150)
            .IsRequired();

        builder
            .Property(product => product.Price)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(product => product.OldPrice)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(product => product.Discount)
            .HasMaxLength(3)
            .IsRequired();

        builder
            .Property(product => product.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder
            .Property(product => product.InStock)
            .HasMaxLength(10000)
            .IsRequired();
        
        builder
            .Property(product => product.ImagePath)
            .HasMaxLength(500)
            .IsRequired();
        
        builder
            .Property(product => product.ImageMiniPath)
            .HasMaxLength(500)
            .IsRequired();

        builder
            .Property(product => product.ImagesListPath)
            .HasMaxLength(500)
            .IsRequired(false);

        builder
            .Property(product => product.Rating)
            .HasMaxLength(1)
            .IsRequired(false);
        
        builder
            .HasMany(product => product.Reviews)
            .WithOne(review => review.Product)
            .HasForeignKey(review => review.ProductId)
            .IsRequired();
        
        builder
            .HasOne(product => product.ProductDetail)
            .WithOne(detail => detail.Product)
            .HasForeignKey<Product>(product => product.ProductDetailId);

        builder
            .HasOne(product => product.Category)
            .WithMany(detail => detail.Products)
            .HasForeignKey(product => product.CategoryId);
        
        builder
            .HasOne(product => product.Seller)
            .WithMany(seller => seller.Products)
            .HasForeignKey(product => product.SellerId);

        builder
            .HasOne(product => product.ClothesProduct)
            .WithOne(clothesProduct => clothesProduct.Product)
            .HasForeignKey<Product>(product => product.ClothesProductId);
        
        builder
            .HasOne(product => product.ShoesProduct)
            .WithOne(clothesProduct => clothesProduct.Product)
            .HasForeignKey<Product>(product => product.ShoesProductId);
    }
}