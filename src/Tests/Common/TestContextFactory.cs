using Microsoft.EntityFrameworkCore;
using RenStore.Domain.Entities;
using RenStore.Persistence;
using RenStore.Persistence.Repository;

namespace Tests.Common;

public class TestContextFactory
{
    public static string ConnectionString = "Server=localhost;Port=5432;DataBase=UnitRenstoreTests; User Id=re;Password=postgres;Include Error Detail=True";
    
    #region constants
    
    public static readonly string ColorNameForCreate = "White";
    public static readonly string ColorNameForUpdate = "Black";
    public static readonly string ColorNameForDelete = "Green";
    public static readonly string ColorNameForGetting1 = "Blue";
    public static readonly string ColorNameForGetting2 = "Purple";
    public static readonly string ColorNameForGetting3 = "Yellow";
    public static readonly string ColorNameForGetting4 = "Red";
    public static readonly string ColorNameForGetting5 = "Gray";
    public static readonly string ColorNameForGetting6 = "DarkGray";
    
    public static readonly Guid AddressIdForCreate = Guid.NewGuid();
    public static readonly Guid AddressIdForUpdate = Guid.NewGuid();
    public static readonly Guid AddressIdForDelete = Guid.NewGuid();
    public static readonly Guid AddressIdForGetting1 = Guid.NewGuid();
    public static readonly Guid AddressIdForGetting2 = Guid.NewGuid();
    public static readonly Guid AddressIdForGetting3 = Guid.NewGuid();
    
    public static readonly long SellerIdForCreate = 3245; 
    public static readonly long SellerIdForUpdate = 1232; 
    public static readonly long SellerIdForDelete = 3422; 
    public static readonly long SellerIdForGetting1 = 1842; 
    public static readonly long SellerIdForGetting2 = 184632; 
    public static readonly long SellerIdForGetting3 = 1733842; 
    public static readonly long SellerIdForGetting4 = 526271842; 
    public static readonly string SellerNameForCreate = nameof(SellerNameForCreate);
    public static readonly string SellerNameForUpdate = "Afegre";
    public static readonly string SellerNameForDelete = "Bgsesege";
    public static readonly string SellerNameForGetting1 = "CSample";
    public static readonly string SellerNameForGetting2 = "Dgege";
    public static readonly string SellerNameForGetting3 = "Egege";
    public static readonly string SellerNameForGetting4 = "Sample";
    
    public static readonly string UserIdForCreateSeller = Guid.NewGuid().ToString();
    public static readonly string UserIdForUpdateSeller = Guid.NewGuid().ToString();
    public static readonly string UserIdForDeleteSeller = Guid.NewGuid().ToString();
    public static readonly string UserIdForGettingSeller1 = Guid.NewGuid().ToString();
    public static readonly string UserIdForGettingSeller2 = Guid.NewGuid().ToString();
    public static readonly string UserIdForGettingSeller3 = Guid.NewGuid().ToString();
    public static readonly string UserIdForGettingSeller4 = Guid.NewGuid().ToString();
    public static readonly string UserIdForGettingSeller5 = Guid.NewGuid().ToString();
    public static readonly string UserIdForGettingSeller6 = Guid.NewGuid().ToString();

    private static readonly ApplicationUser _userForDelete = new ApplicationUser
    {
        Id = Guid.NewGuid().ToString(),
        Name = "testm5323ail@.com",
        UserName = "testm5323ail@.com",
        Email = "testm5323ail@.com",
        PhoneNumber = "5323620243",
        PasswordHash = Guid.NewGuid().ToString(),
        Role = "User",
        CreatedDate = DateTime.UtcNow,
    };

    private static readonly ApplicationUser _userForGetting1 =
        new ApplicationUser()
        {
            Id = UserIdForGettingSeller1,
            Name = "7testmail@.com",
            UserName = "7testmail@.com",
            Email = "7testmail@.com",
            PhoneNumber = "0888888888",
            PasswordHash = Guid.NewGuid().ToString(),
            Role = "User",
            CreatedDate = DateTime.UtcNow,
        };

    private static readonly ApplicationUser _userForGetting2 =
        new ApplicationUser()
        {
            Id = UserIdForGettingSeller2,
            Name = "4testmail@.com",
            UserName = "4testmail@.com",
            Email = "4testmail@.com",
            PhoneNumber = "0888888884",
            PasswordHash = Guid.NewGuid().ToString(),
            Role = "User",
            CreatedDate = DateTime.UtcNow,
        };

    private static readonly ApplicationUser _userForGetting3 =
        new ApplicationUser()
        {
            Id = UserIdForGettingSeller3,
            Name = "5testmail@.com",
            UserName = "5testmail@.com",
            Email = "5testmail@.com",
            PhoneNumber = "0888888885",
            PasswordHash = Guid.NewGuid().ToString(),
            Role = "User",
            CreatedDate = DateTime.UtcNow,
        };

    private static readonly ApplicationUser _userForGetting4 =
        new ApplicationUser()
        {
            Id = UserIdForGettingSeller4,
            Name = "6testmail@.com",
            UserName = "6testmail@.com",
            Email = "6testmail@.com",
            PhoneNumber = "0888888886",
            PasswordHash = Guid.NewGuid().ToString(),
            Role = "User",
            CreatedDate = DateTime.UtcNow.AddHours(1),
        };

    #endregion
    
    public static ApplicationDbContext CreateReadyContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        var context = new ApplicationDbContext(options);
        
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        context.Colors.RemoveRange(context.Colors);
        context.Sellers.RemoveRange(context.Sellers);
        context.AspNetUsers.RemoveRange(context.AspNetUsers);

        AddColorsTestData(context);
        AddUsersTestData(context);
        AddSellersTestData(context);
        context.SaveChanges();
        
        context.ChangeTracker.Clear();
        
        return context;
    }
    
    private static void AddColorsTestData(ApplicationDbContext context)
    {
        var colors = new[]
        {
            new ColorEntity()
            {
                Id = Constants.ColorIdForUpdate,
                Name = ColorNameForUpdate,
                NormalizedName = ColorNameForUpdate.ToUpper(),
                NameRu = "колорНейм1",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            },
            new ColorEntity()
            {
                Id = Constants.ColorIdForDelete,
                Name = ColorNameForDelete,
                NormalizedName = ColorNameForDelete.ToUpper(),
                NameRu = "колорНейм2",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            },
            new ColorEntity()
            {
                Id = Constants.ColorIdForGetting1,
                Name = ColorNameForGetting1,
                NormalizedName = ColorNameForGetting1.ToUpper(),
                NameRu = "колорНейм3",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            },
            new ColorEntity()
            {
                Id = Constants.ColorIdForGetting2,
                Name = ColorNameForGetting2,
                NormalizedName = ColorNameForGetting2.ToUpper(),
                NameRu = "колорНейм4",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            },
            new ColorEntity()
            {
                Id = Constants.ColorIdForGetting3,
                Name = ColorNameForGetting3,
                NormalizedName = ColorNameForGetting3.ToUpper(),
                NameRu = "колорНейм5",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            }
            ,
            new ColorEntity()
            {
                Id = Constants.ColorIdForGetting4,
                Name = ColorNameForGetting4,
                NormalizedName = ColorNameForGetting4.ToUpper(),
                NameRu = "колорНейм6",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            }
            ,
            new ColorEntity()
            {
                Id = Constants.ColorIdForGetting5,
                Name = ColorNameForGetting5,
                NormalizedName = ColorNameForGetting5.ToUpper(),
                NameRu = "колорНейм7",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            },
            new ColorEntity()
            {
                Id = Constants.ColorIdForGetting6,
                Name = ColorNameForGetting6,
                NormalizedName = ColorNameForGetting6.ToUpper(),
                NameRu = "колорНейм8",
                ColorCode = "#123",
                Description = Guid.NewGuid().ToString(),
            }
        };
        
        context.Colors.AddRange(colors); 
    }
    
    private static void AddUsersTestData(ApplicationDbContext context)
    {
        var users = new[]
        {   
            // For Create
            new ApplicationUser
            {
                Id = UserIdForCreateSeller,
                Name = "1testmail@.com",
                UserName = "1testmail@.com",
                Email = "1testmail@.com",
                PhoneNumber = "0888888881",
                PasswordHash = Guid.NewGuid().ToString(),
                Role = "User",
                CreatedDate = DateTime.UtcNow,
            },
            // For Update
            new ApplicationUser()
            {
                Id = UserIdForUpdateSeller,
                Name = "2testmail@.com",
                UserName = "2testmail@.com",
                Email = "2testmail@.com",
                PhoneNumber = "0888888882",
                PasswordHash = Guid.NewGuid().ToString(),
                Role = "User",
                CreatedDate = DateTime.UtcNow,
            },
            // For Delete
            new ApplicationUser()
            {
                Id = UserIdForDeleteSeller,
                Name = "3testmail@.com",
                UserName = "3testmail@.com",
                Email = "3testmail@.com",
                PhoneNumber = "0888888883",
                PasswordHash = Guid.NewGuid().ToString(),
                Role = "User",
                CreatedDate = DateTime.UtcNow,
            },
            
        };
        
        context.AspNetUsers.AddRange(users);
    }

    private static void AddSellersTestData(ApplicationDbContext context)
    {
        var sellers = new[]
        {
            // For Delete
            new SellerEntity()
            {
                Id = SellerIdForDelete,
                Name = SellerNameForDelete,
                Description = "Sample Description for Update",
                NormalizedName = SellerNameForDelete.ToUpper(),
                CreatedDate = DateTime.UtcNow,
                ApplicationUser = _userForDelete,
                ApplicationUserId = UserIdForDeleteSeller,
                IsBlocked = false
            },
            // For Update
            new SellerEntity()
            {
                Id = SellerIdForUpdate,
                Name = SellerNameForUpdate,
                Description = "Sample Description for Update",
                NormalizedName = SellerNameForUpdate.ToUpper(),
                CreatedDate = DateTime.UtcNow,
                ApplicationUserId = UserIdForUpdateSeller,
            },
            new SellerEntity()
            {
                Id = SellerIdForGetting1,
                Name = SellerNameForGetting1,
                Description = "Sample Description for Update",
                NormalizedName = SellerNameForGetting1.ToUpper(),
                CreatedDate = DateTime.UtcNow,
                ApplicationUser = _userForGetting1,
                ApplicationUserId = UserIdForGettingSeller1,
                IsBlocked = false
            },
            new SellerEntity()
            {
                Id = SellerIdForGetting2,
                Name = SellerNameForGetting2,
                Description = "Sample Description for Update",
                NormalizedName = SellerNameForGetting2.ToUpper(),
                CreatedDate = DateTime.UtcNow,
                ApplicationUser = _userForGetting2,
                ApplicationUserId = UserIdForGettingSeller2,
                IsBlocked = true
            },
            new SellerEntity()
            {
                Id = SellerIdForGetting3,
                Name = SellerNameForGetting3,
                Description = "Sample Description for Update",
                NormalizedName = SellerNameForGetting3.ToUpper(),
                CreatedDate = DateTime.UtcNow,
                ApplicationUser = _userForGetting3,
                ApplicationUserId = UserIdForGettingSeller3,
                IsBlocked = false
            },
            new SellerEntity()
            {
                Id = SellerIdForGetting4,
                Name = SellerNameForGetting4,
                Description = "Sample Description for Update",
                NormalizedName = SellerNameForGetting4.ToUpper(),
                CreatedDate = DateTime.UtcNow,
                ApplicationUser = _userForGetting4,
                ApplicationUserId = UserIdForGettingSeller4,
                IsBlocked = true
            },
        };
        context.Sellers.AddRange(sellers);
    }
}