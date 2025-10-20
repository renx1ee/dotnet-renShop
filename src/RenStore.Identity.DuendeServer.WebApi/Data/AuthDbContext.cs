using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RenStore.Domain.Entities;
using RenStore.Domain.Entities.Products;
using RenStore.Persistence.EntityTypeConfigurations;

namespace RenStore.Identity.DuendeServer.WebAPI.Data;

public class AuthDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{ 
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>(entity =>
            entity.ToTable(name: "AspNetUsers"));
        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            entity.ToTable(name: "UserRoles"));
        modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            entity.ToTable(name: "UserClaim"));
        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            entity.ToTable(name: "UserLogins"));
        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            entity.ToTable(name: "UserTokens"));
        modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            entity.ToTable(name: "RoleClaim"));
        
        /*modelBuilder.ApplyConfiguration(new ProductConfiguration());*/
        modelBuilder.ApplyConfiguration(new ProductDetailConfiguration());
        modelBuilder.ApplyConfiguration(new ClothesProductConfiguration());
        modelBuilder.ApplyConfiguration(new ShoesProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        /*modelBuilder.ApplyConfiguration(new SellerConfiguration());*/
        modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        modelBuilder.ApplyConfiguration(new ShoppingCartItemConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
    
    public async Task<int> SaveChangesAsync() 
    {
        return await base.SaveChangesAsync();
    }
    
    public DbSet<ApplicationUser> AspNetUsers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductDetail> ProductDetails { get; set; }
    public DbSet<ClothesProduct> ClothesProducts { get; set; }
    public DbSet<ShoesProduct> ShoesProducts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<ProductQuestion> ProductQuestions { get; set; }
    public DbSet<ProductAnswer> ProductAnswers { get; set; }
}