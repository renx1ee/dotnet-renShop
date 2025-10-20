using Microsoft.EntityFrameworkCore;
using RenStore.Domain.Entities;
using RenStore.Persistence;

namespace Tests.Common;

public class TestContextFactory
{
    public static string ConnectionString = "Server=localhost;Port=5432;DataBase=UnitRenstoreTests; User Id=re;Password=postgres;Include Error Detail=True";
    
    public static readonly int ColorIdForUpdate = 1;
    public static readonly int ColorIdForDelete = 2;
    public static readonly int ColorIdForGetting1 = 3;
    public static readonly int ColorIdForGetting2 = 4;
    public static readonly int ColorIdForGetting3 = 5;
    public static readonly int ColorIdForGetting4 = 6;
    public static readonly int ColorIdForGetting5 = 7;
    public static readonly int ColorIdForGetting6 = 8;
    
    public static readonly string ColorNameForCreate = "White";
    public static readonly string ColorNameForUpdate = "Black";
    public static readonly string ColorNameForDelete = "Green";
    public static readonly string ColorNameForGetting1 = "Blue";
    public static readonly string ColorNameForGetting2 = "Purple";
    public static readonly string ColorNameForGetting3 = "Yellow";
    public static readonly string ColorNameForGetting4 = "Red";
    public static readonly string ColorNameForGetting5 = "Gray";
    public static readonly string ColorNameForGetting6 = "DarkGray";
    
    public static readonly long SellerIdForCreate = 3245;
    public static readonly long SellerIdForUpdate = 1232;
    public static readonly long SellerIdForDelete = 3422;
    public static readonly long SellerIdForGetting = 1842;
    public static readonly Guid UserIdForGettingSeller = Guid.NewGuid();
    public static readonly string SellerNameForGetting = nameof(SellerNameForGetting);
    
    public static ApplicationDbContext CreateReadyContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        var context = new ApplicationDbContext(options);
        
        context.Database.EnsureCreated();
        
        context.Colors.RemoveRange(context.Colors);

        var colors = new[]
        {
            new Color()
            {
                Id = ColorIdForUpdate,
                Name = ColorNameForUpdate,
                NormalizedName = ColorNameForUpdate.ToUpper(),
                NameRu = "колорНейм1",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            },
            new Color()
            {
                Id = ColorIdForDelete,
                Name = ColorNameForDelete,
                NormalizedName = ColorNameForDelete.ToUpper(),
                NameRu = "колорНейм2",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            },
            new Color()
            {
                Id = ColorIdForGetting1,
                Name = ColorNameForGetting1,
                NormalizedName = ColorNameForGetting1.ToUpper(),
                NameRu = "колорНейм3",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            },
            new Color()
            {
                Id = ColorIdForGetting2,
                Name = ColorNameForGetting2,
                NormalizedName = ColorNameForGetting2.ToUpper(),
                NameRu = "колорНейм4",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            },
            new Color()
            {
                Id = ColorIdForGetting3,
                Name = ColorNameForGetting3,
                NormalizedName = ColorNameForGetting3.ToUpper(),
                NameRu = "колорНейм5",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            }
            ,
            new Color()
            {
                Id = ColorIdForGetting4,
                Name = ColorNameForGetting4,
                NormalizedName = ColorNameForGetting4.ToUpper(),
                NameRu = "колорНейм6",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            }
            ,
            new Color()
            {
                Id = ColorIdForGetting5,
                Name = ColorNameForGetting5,
                NormalizedName = ColorNameForGetting5.ToUpper(),
                NameRu = "колорНейм7",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            },
            new Color()
            {
                Id = ColorIdForGetting6,
                Name = ColorNameForGetting6,
                NormalizedName = ColorNameForGetting6.ToUpper(),
                NameRu = "колорНейм8",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            }
        };

        var users = new[]
        {
            new ApplicationUser()
            {
                Id = UserIdForGettingSeller.ToString(),
            },
            new ApplicationUser()
            {
                
            },
            new ApplicationUser()
            {
                
            }
        };
        
        /*var sellers = new[]
        {
            new Seller()
            {
                Id = SellerIdForUpdate,
                Name = "Sample Name for Update",
                Description = "Sample Description for Update",
                NormalizedName = SellerNameForGetting.ToUpper(),
                CreatedDate = DateTime.UtcNow,
                ApplicationUserId = UserIdForGettingSeller,
                User = users[0],
                IsBlocked = false
            },
            new Seller()
            {
                Id = SellerIdForDelete,
                Name = "Sample Name for Delete",
                Description = "Sample Description for Update",
                NormalizedName = SellerNameForGetting.ToUpper(),
                CreatedDate = DateTime.UtcNow,
                User = users[1],
                IsBlocked = false
            },
            new Seller()
            {
                Id = SellerIdForGetting,
                Name = SellerNameForGetting,
                Description = "Sample Description for Update",
                NormalizedName = SellerNameForGetting.ToUpper(),
                CreatedDate = DateTime.UtcNow,
                User = users[2],
                IsBlocked = false
            },
        };
        
        
        context.Sellers.AddRange(sellers);*/
        context.Colors.AddRange(colors);
        context.SaveChanges();
        
        context.ChangeTracker.Clear();
        
        return context;
    }
}

/*public class TestContextFactory
{
    public static string ConnectionString = "Server=localhost;Port=5432;DataBase=UnitTestsDb; User Id=re;Password=postgres ;Include Error Detail=True";
    
    public static readonly int CategoryIdForUpdate = 12345;
    public static readonly int CategoryIdForDelete = 55673;
    public static readonly int CategoryIdForGetting = 63232;
    public static readonly string CategoryNameForGetting = nameof(CategoryNameForGetting);
    
    public static readonly Guid OrderIdForUpdate = Guid.NewGuid();
    public static readonly Guid OrderIdForDelete = Guid.NewGuid();
    public static readonly Guid OrderIdForGetting = Guid.NewGuid();
    
    public static readonly string UserIdForGetting = Guid.NewGuid().ToString();
    
    public static readonly Guid ProductIdForGetting = Guid.NewGuid();

    public static readonly int SellerIdForUpdate = 1232;
    public static readonly int SellerIdForDelete = 3422;
    public static readonly int SellerIdForGetting = 1842;
    public static readonly string SellerNameForGetting = nameof(SellerNameForGetting);

    public static ApplicationDbContext CreateReadyContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        var context = new ApplicationDbContext(options);

        context.Database.EnsureCreated();

        context.Categories.RemoveRange(context.Categories);
        context.Sellers.RemoveRange(context.Sellers);

        var users = new[]
        {
            new ApplicationUser
            {
                Id = UserIdForGetting,
                Name = "testmail@.com",
                Email = "testmail@.com",
                PhoneNumber = "088888888",
                Role = "User",
                Country = "TestCountry",
                City = "TestCity",
                CreatedDate = DateTime.UtcNow,
            }
        };

        var products = new[]
        {
            new Product()
            {
                Id = ProductIdForGetting,
                ProductName = "Product for getting",
                Price = 100,
                OldPrice = 150,
                Discount = 50,
                Description = "Product for getting",
                InStock = 5,
                ImagePath = "fwwfwew",
                ImageMiniPath = "wfwefewefe",
                Rating = 0,
                CategoryId = CategoryIdForGetting,
                SellerId = SellerIdForGetting,
                /*ProductDetailId = null,
                ClothesProductId = null,
                ShoesProductId = null,#1#
                CreatedDate = DateTime.UtcNow,
            }
        };

        var sellers = new[]
        {
            new Seller()
            {
                Id = SellerIdForUpdate,
                Name = "Sample Name for Update",
                Description = "Sample Description for Update",
            },
            new Seller()
            {
                Id = SellerIdForDelete,
                Name = "Sample Name for Delete",
                Description = "Sample Description for Delete",
            },
            new Seller()
            {
                Id = SellerIdForGetting,
                Name = SellerNameForGetting,
                Description = "Sample Description for Getting",
            },
        };

        var categories = new[]
        {
            new Category
            {
                Id = CategoryIdForUpdate,
                Name = "Category for update",
                Description = "Category for update description",
                ImagePath = "/images/category/delete_category.img"
            },
            new Category
            {
                Id = CategoryIdForDelete,
                Name = "Category for delete",
                Description = "Category for Delete description",
                ImagePath = "/images/category/delete_category.img"
            },
            new Category
            {
                Id = CategoryIdForGetting,
                Name = CategoryNameForGetting,
                Description = "Test Description",
                ImagePath = "/images/category/test_category.img"
            },
            new Category
            {
                Id = 234234,
                Name = "Clothes",
                Description = "Category for update description",
                ImagePath = "/images/category/delete_category.img"
            },
        };

        var orders = new[]
        {
            
            new Order
            {
                Id = OrderIdForUpdate,
                Address = "Sample Address for update",
                City = "Sample City for update",
                Country = "Sample Country for update",
                Amount = 1,
                ZipCode = 222222,
                Status = DeliveryStatus.AwaitingConfirmation,
                OrderTotal = 321312,
                CreatedDate = DateTime.UtcNow,
                ApplicationUserId = UserIdForGetting,
                ProductId = ProductIdForGetting
            },
            new Order
            {
                Id = OrderIdForDelete,
                Address = "Sample Address for delete",
                City = "Sample City for delete",
                Country = "Sample Country for delete",
                Amount = 1,
                ZipCode = 222222,
                Status = DeliveryStatus.AwaitingConfirmation,
                OrderTotal = 321312,
                CreatedDate = DateTime.UtcNow,
                ApplicationUserId = UserIdForGetting,
                ProductId = ProductIdForGetting
            },
            new Order
            {
                Id = OrderIdForGetting,
                Address = "Sample Address for getting",
                City = "Sample City for getting",
                Country = "Sample Country for getting",
                Amount = 1,
                ZipCode = 111111,
                Status = DeliveryStatus.AwaitingConfirmation,
                OrderTotal = 321312,
                CreatedDate = DateTime.UtcNow,
                ApplicationUserId = UserIdForGetting,
                ProductId = ProductIdForGetting
            },
        };
        
        context.Categories.AddRange(categories);
        
        context.SaveChanges();
        
        return context;
    }
}*/