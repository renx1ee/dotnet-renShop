using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RenStore.Microservice.Notification.Configs;

public class NotificationEntityTypeConfiguration : IEntityTypeConfiguration<Models.Notification>
{
    public void Configure(EntityTypeBuilder<Models.Notification> builder)
    {
        throw new NotImplementedException();
    }
}