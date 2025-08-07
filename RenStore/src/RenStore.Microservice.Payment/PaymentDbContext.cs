using Microsoft.EntityFrameworkCore;
using RenStore.Microservice.Payment.Models;

namespace RenStore.Microservice.Payment;

public class PaymentDbContext : DbContext
{
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) 
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
    
    public DbSet<Models.Payment> Payments { get; set; }
    public DbSet<Loggs> Loggs { get; set; }
    public DbSet<Address> Addresses { get; set; }
}