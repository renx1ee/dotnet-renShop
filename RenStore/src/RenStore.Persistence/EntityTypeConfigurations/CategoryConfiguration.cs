using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.EntityTypeConfigurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(category => category.Id);

        builder
            .Property(category => category.Name)
            .HasMaxLength(30)
            .IsRequired();
        
        builder
            .Property(category => category.Description)
            .HasMaxLength(200)
            .IsRequired(false);
        
        builder
            .Property(category => category.ImagePath)
            .HasMaxLength(200)
            .IsRequired();
        
        builder
            .HasMany(category => category.Products)
            .WithOne(product => product.Category)
            .HasForeignKey(product => product.CategoryId);
            
    }
}