using Microsoft.EntityFrameworkCore;
using RenStore.Microservice.Payment.Models;

namespace RenStore.Microservice.Payment;

public class PaymentDbContext(DbContextOptions<DbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
    
    public DbSet<Models.Payment> Payments { get; set; }
    public DbSet<Loggs> Loggs { get; set; }
    public DbSet<Address> Addresses { get; set; }
}