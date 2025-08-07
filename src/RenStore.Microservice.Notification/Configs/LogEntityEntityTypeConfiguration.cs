using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenStore.Microservice.Notification.Models;

namespace RenStore.Microservice.Notification.Configs;

public class LogEntityEntityTypeConfiguration : IEntityTypeConfiguration<LogEntity>
{
    public void Configure(EntityTypeBuilder<LogEntity> builder)
    {
    }
}