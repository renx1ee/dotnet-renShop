using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.EntityTypeConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(order => order.Id);
        
        builder
            .Property(order => order.Address)
            .HasMaxLength(100)
            .IsRequired();
        
        builder
            .Property(order => order.City)
            .HasMaxLength(25)
            .IsRequired();
        
        builder
            .Property(order => order.Country)
            .HasMaxLength(25)
            .IsRequired();
        
        builder
            .Property(order => order.Amount)
            .HasMaxLength(5)
            .IsRequired();
        
        builder
            .Property(order => order.ZipCode)
            .HasMaxLength(6)
            .IsRequired();

        builder
            .Property(order => order.Status)
            .IsRequired();
        
        builder
            .Property(order => order.OrderTotal)
            .HasMaxLength(10)
            .IsRequired();
        
        builder
            .Property(order => order.CreatedDate)
            .IsRequired();
        
        builder
            .Property(order => order.ApplicationUserId)
            .IsRequired();
        
        builder
            .Property(order => order.ProductId)
            .IsRequired();
    }
}