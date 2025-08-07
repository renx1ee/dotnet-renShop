using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.EntityTypeConfigurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(review => review.Id);
        
        builder
            .Property(review => review.CreatedDate)
            .IsRequired();
        
        builder
            .Property(review => review.LastUpdatedDate)
            .IsRequired(false);
        
        builder
            .Property(review => review.IsUpdated)
            .HasDefaultValue(false)
            .IsRequired();

        builder
            .Property(review => review.Message)
            .HasMaxLength(500)
            .IsRequired(false);
        
        builder
            .Property(review => review.Rating)
            .HasMaxLength(5)
            .IsRequired();

        builder
            .Property(review => review.ImagesUrls)
            .IsRequired(false);
        
        builder
            .Property(review => review.LikesCount)
            .HasDefaultValue(0)
            .IsRequired();

        builder
            .Property(review => review.UsersLikedIds)
            .IsRequired(false);
        
        builder
            .Property(review => review.ApplicationUserId)
            .IsRequired();

        builder
            .HasOne(review => review.Product)
            .WithMany(product => product.Reviews)
            .HasForeignKey(review => review.ProductId);
    }   
}