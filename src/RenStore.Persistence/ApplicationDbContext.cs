using Microsoft.EntityFrameworkCore;
using RenStore.Domain.Entities;
using RenStore.Persistence.EntityTypeConfigurations;

namespace RenStore.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ColorConfiguration());
        modelBuilder.ApplyConfiguration(new SellerConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new SubCategoryConfiguration());

        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<ApplicationUser> AspNetUsers { get; set; }
    public DbSet<SellerEntity> Sellers { get; set; }
    
    public DbSet<ColorEntity> Colors { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<SubCategoryEntity> SubCategories { get; set; }
    public DbSet<CityEntity> Cities { get; set; }
}
