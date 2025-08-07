using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenStore.Domain.Entities.Products;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.EntityTypeConfigurations;

public class ShoesProductConfiguration : IEntityTypeConfiguration<ShoesProduct>
{
    public void Configure(EntityTypeBuilder<ShoesProduct> builder)
    {
        builder.HasKey(shoes => shoes.Id);
        
        builder
            .Property(shoes => shoes.FullnessOfShoes)
            .IsRequired();
        
        builder
            .Property(shoes => shoes.ShoeInsoleMaterial)
            .IsRequired();
        
        builder
            .Property(shoes => shoes.ShoeLiningMaterial)
            .IsRequired();
        
        builder
            .Property(shoes => shoes.ShoeSoleMaterial)
            .IsRequired();
        
        builder
            .Property(shoes => shoes.SoleFasteningMethod)
            .IsRequired();
        
        builder
            .Property(shoes => shoes.TypeOfFastener)
            .IsRequired();
        
        builder
            .Property(shoes => shoes.SoleHeight)
            .HasMaxLength(3)
            .IsRequired();
        
        builder
            .Property(shoes => shoes.Gender)
            .IsRequired();
        
        builder
            .Property(shoes => shoes.Season)
            .IsRequired();
        
        builder
            .Property(shoes => shoes.TakingCareOfThings)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .HasOne(shoes => shoes.Product)
            .WithOne(product => product.ShoesProduct)
            .HasForeignKey<ShoesProduct>(shoes => shoes.ProductId);
    }
}