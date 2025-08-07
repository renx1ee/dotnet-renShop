using Microsoft.EntityFrameworkCore;
using RenStore.Microservices.Bonuses.Models;

namespace RenStore.Microservices.Bonuses;

public class BonusDbContext : DbContext   
{
    public BonusDbContext(DbContextOptions<BonusDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    }

    public DbSet<Bonus> Bonus { get; set; }
    public DbSet<BonusActivation> BonusActivations { get; set; }
}