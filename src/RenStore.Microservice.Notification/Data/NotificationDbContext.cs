using Microsoft.EntityFrameworkCore;
using RenStore.Microservice.Notification.Models;

namespace RenStore.Microservice.Notification.Data;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }
    
    public DbSet<Models.Notification> Notifications { get; set; }    
    public DbSet<Message> Messages { get; set; }

}