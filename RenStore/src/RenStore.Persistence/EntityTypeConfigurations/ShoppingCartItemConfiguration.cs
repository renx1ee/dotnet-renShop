using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.EntityTypeConfigurations;

public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
        builder.HasKey(item => item.Id);
        
        builder
            .Property(item => item.Amount)
            .HasMaxLength(10)
            .HasDefaultValue(1)
            .IsRequired();

        builder
            .HasOne(item => item.Product);
    }
}