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
        context.Countries.RemoveRange(context.Countries);
        context.SubCategories.RemoveRange(context.SubCategories);
        context.Categories.RemoveRange(context.Categories);

        AddColorsTestData(context);
        AddUsersTestData(context);
        AddSellersTestData(context);
        AddCountriesTestData(context);
        AddCategoriesTestData(context);
        AddSubCategoriesTestData(context);
        AddCitiesTestData(context);
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
                CreatedDate = DateTime.UtcNow.AddHours(1),
                ApplicationUser = _userForGetting4,
                ApplicationUserId = UserIdForGettingSeller4,
                IsBlocked = true
            },
        };
        context.Sellers.AddRange(sellers);
    }

    private static void AddCountriesTestData(ApplicationDbContext context)
    {
        var countries = new[]
        {
            // For delete
            new CountryEntity()
            {
                Id = Constants.CountryIdForDelete,
                Name = Constants.CountryNameForDelete,
                NormalizedName = Constants.CountryNameForDelete.ToUpper(),
                NameRu = Constants.CountryNameRuForDelete,
                NormalizedNameRu = Constants.CountryNameRuForDelete.ToUpper(),
                Code =  "del"
            },
            // For update
            new CountryEntity()
            {
                Id = Constants.CountryIdForUpdate,
                Name = Constants.CountryNameForUpdate,
                NormalizedName = Constants.CountryNameForUpdate.ToUpper(),
                NameRu = Constants.CountryNameRuForUpdate,
                NormalizedNameRu = Constants.CountryNameRuForUpdate.ToUpper(),
                Code =  "upd"
            },
            new CountryEntity()
            {
                Id = Constants.CountryIdForGetting1,
                Name = Constants.CountryNameForGetting1,
                NormalizedName = Constants.CountryNameForGetting1.ToUpper(),
                NameRu = Constants.CountryNameRuForGetting1,
                NormalizedNameRu = Constants.CountryNameRuForGetting1.ToUpper(),
                Code =  "get1"
            },
            new CountryEntity()
            {
                Id = Constants.CountryIdForGetting2,
                Name = Constants.CountryNameForGetting2,
                NormalizedName = Constants.CountryNameForGetting2.ToUpper(),
                NameRu = Constants.CountryNameRuForGetting2,
                NormalizedNameRu = Constants.CountryNameRuForGetting2.ToUpper(),
                Code =  "get2"
            },
            new CountryEntity()
            {
                Id = Constants.CountryIdForGetting3,
                Name = Constants.CountryNameForGetting3,
                NormalizedName = Constants.CountryNameForGetting3.ToUpper(),
                NameRu = Constants.CountryNameRuForGetting3,
                NormalizedNameRu = Constants.CountryNameRuForGetting3.ToUpper(),
                Code =  "get3"
            },
            new CountryEntity()
            {
                Id = Constants.CountryIdForGetting4,
                Name = Constants.CountryNameForGetting4,
                NormalizedName = Constants.CountryNameForGetting4.ToUpper(),
                NameRu = Constants.CountryNameRuForGetting4,
                NormalizedNameRu = Constants.CountryNameRuForGetting4.ToUpper(),
                Code =  "get4",
                OtherName = Constants.CountryOtherNameForGetting4,
                NormalizedOtherName = Constants.CountryOtherNameForGetting4.ToUpper(),
            },
            new CountryEntity()
            {
                Id = Constants.CountryIdForGetting5,
                Name = Constants.CountryNameForGetting5,
                NormalizedName = Constants.CountryNameForGetting5.ToUpper(),
                NameRu = Constants.CountryNameRuForGetting5,
                NormalizedNameRu = Constants.CountryNameRuForGetting5.ToUpper(),
                Code =  "get5",
            },
            new CountryEntity()
            {
                Id = Constants.CountryIdForGetting6,
                Name = Constants.CountryNameForGetting6,
                NormalizedName = Constants.CountryNameForGetting6.ToUpper(),
                NameRu = Constants.CountryNameRuForGetting6,
                NormalizedNameRu = Constants.CountryNameRuForGetting6.ToUpper(),
                Code =  "get6",
                OtherName = Constants.CountryOtherNameForGetting6,
                NormalizedOtherName = Constants.CountryOtherNameForGetting6.ToUpper(),
            }
        };
        
        context.Countries.AddRange(countries);
    }

    private static void AddCategoriesTestData(ApplicationDbContext context)
    {
        var items = new []
        {
            new CategoryEntity()
            {
                Id = Constants.CategoryIdForUpdate,
                Name = Constants.CategoryNameForUpdate,
                NormalizedName = Constants.CategoryNameForUpdate.ToUpper(),
                NameRu = Constants.CategoryNameRuForUpdate,
                NormalizedNameRu = Constants.CategoryNameRuForUpdate.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now
            },
            new CategoryEntity()
            {
                Id = Constants.CategoryIdForDelete,
                Name = Constants.CategoryNameForDelete,
                NormalizedName = Constants.CategoryNameForDelete.ToUpper(),
                NameRu = Constants.CategoryNameRuForDelete,
                NormalizedNameRu = Constants.CategoryNameRuForDelete.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now
            },
            new CategoryEntity()
            {
                Id = Constants.CategoryIdForGetting1,
                Name = Constants.CategoryNameForGetting1,
                NormalizedName = Constants.CategoryNameForGetting1.ToUpper(),
                NameRu = Constants.CategoryNameRuForGetting1,
                NormalizedNameRu = Constants.CategoryNameRuForGetting1.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now
            },
            new CategoryEntity()
            {
                Id = Constants.CategoryIdForGetting2,
                Name = Constants.CategoryNameForGetting2,
                NormalizedName = Constants.CategoryNameForGetting2.ToUpper(),
                NameRu = Constants.CategoryNameRuForGetting2,
                NormalizedNameRu = Constants.CategoryNameRuForGetting2.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now
            },
            new CategoryEntity()
            {
                Id = Constants.CategoryIdForGetting3,
                Name = Constants.CategoryNameForGetting3,
                NormalizedName = Constants.CategoryNameForGetting3.ToUpper(),
                NameRu = Constants.CategoryNameRuForGetting3,
                NormalizedNameRu = Constants.CategoryNameRuForGetting3.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now
            },
            new CategoryEntity()
            {
                Id = Constants.CategoryIdForGetting4,
                Name = Constants.CategoryNameForGetting4,
                NormalizedName = Constants.CategoryNameForGetting4.ToUpper(),
                NameRu = Constants.CategoryNameRuForGetting4,
                NormalizedNameRu = Constants.CategoryNameRuForGetting4.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now
            },
            new CategoryEntity()
            {
                Id = Constants.CategoryIdForGetting5,
                Name = Constants.CategoryNameForGetting5,
                NormalizedName = Constants.CategoryNameForGetting5.ToUpper(),
                NameRu = Constants.CategoryNameRuForGetting5,
                NormalizedNameRu = Constants.CategoryNameRuForGetting5.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now
            },
            new CategoryEntity()
            {
                Id = Constants.CategoryIdForGetting6,
                Name = Constants.CategoryNameForGetting6,
                NormalizedName = Constants.CategoryNameForGetting6.ToUpper(),
                NameRu = Constants.CategoryNameRuForGetting6,
                NormalizedNameRu = Constants.CategoryNameRuForGetting6.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now
            },
            new CategoryEntity()
            {
                Id = Constants.CategoryIdForGetting7,
                Name = Constants.CategoryNameForGetting7,
                NormalizedName = Constants.CategoryNameForGetting7.ToUpper(),
                NameRu = Constants.CategoryNameRuForGetting7,
                NormalizedNameRu = Constants.CategoryNameRuForGetting7.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now
            },
        };
        context.Categories.AddRange(items);
    }

    private static void AddSubCategoriesTestData(ApplicationDbContext context)
    {
        var subCategories = new[]
        {
            new SubCategoryEntity()
            {
                Id = Constants.SubCategoryIdForUpdate,
                Name = Constants.SubCategoryNameForUpdate,
                NormalizedName = Constants.SubCategoryNameForUpdate.ToUpper(),
                NameRu = Constants.SubCategoryNameRuForUpdate,
                NormalizedNameRu = Constants.SubCategoryNameRuForUpdate.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now,
                CategoryId = Constants.CategoryIdForUpdate,
            },
            new SubCategoryEntity()
            {
                Id = Constants.SubCategoryIdForDelete,
                Name = Constants.SubCategoryNameForDelete,
                NormalizedName = Constants.SubCategoryNameForDelete.ToUpper(),
                NameRu = Constants.SubCategoryNameRuForDelete,
                NormalizedNameRu = Constants.SubCategoryNameRuForDelete.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now,
                CategoryId = Constants.CategoryIdForDelete,
            },
            new SubCategoryEntity()
            {
                Id = Constants.SubCategoryIdForGetting1,
                Name = Constants.SubCategoryNameForGetting1,
                NormalizedName = Constants.SubCategoryNameForGetting1.ToUpper(),
                NameRu = Constants.SubCategoryNameRuForGetting1,
                NormalizedNameRu = Constants.SubCategoryNameRuForGetting1.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now,
                CategoryId = Constants.CategoryIdForGetting1,
            },
            new SubCategoryEntity()
            {
                Id = Constants.SubCategoryIdForGetting2,
                Name = Constants.SubCategoryNameForGetting2,
                NormalizedName = Constants.SubCategoryNameForGetting2.ToUpper(),
                NameRu = Constants.SubCategoryNameRuForGetting2,
                NormalizedNameRu = Constants.SubCategoryNameRuForGetting2.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now,
                CategoryId = Constants.CategoryIdForGetting2,
            },
            new SubCategoryEntity()
            {
                Id = Constants.SubCategoryIdForGetting3,
                Name = Constants.SubCategoryNameForGetting3,
                NormalizedName = Constants.SubCategoryNameForGetting3.ToUpper(),
                NameRu = Constants.SubCategoryNameRuForGetting3,
                NormalizedNameRu = Constants.SubCategoryNameRuForGetting3.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now,
                CategoryId = Constants.CategoryIdForGetting3,
            },
            new SubCategoryEntity()
            {
                Id = Constants.SubCategoryIdForGetting4,
                Name = Constants.SubCategoryNameForGetting4,
                NormalizedName = Constants.SubCategoryNameForGetting4.ToUpper(),
                NameRu = Constants.SubCategoryNameRuForGetting4,
                NormalizedNameRu = Constants.SubCategoryNameRuForGetting4.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now,
                CategoryId = Constants.CategoryIdForGetting4,
            },
            new SubCategoryEntity()
            {
                Id = Constants.SubCategoryIdForGetting5,
                Name = Constants.SubCategoryNameForGetting5,
                NormalizedName = Constants.SubCategoryNameForGetting5.ToUpper(),
                NameRu = Constants.SubCategoryNameRuForGetting5,
                NormalizedNameRu = Constants.SubCategoryNameRuForGetting5.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now,
                CategoryId = Constants.CategoryIdForGetting5,
            },
            new SubCategoryEntity()
            {
                Id = Constants.SubCategoryIdForGetting6,
                Name = Constants.SubCategoryNameForGetting6,
                NormalizedName = Constants.SubCategoryNameForGetting6.ToUpper(),
                NameRu = Constants.SubCategoryNameRuForGetting6,
                NormalizedNameRu = Constants.SubCategoryNameRuForGetting6.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now,
                CategoryId = Constants.CategoryIdForGetting6,
            },
            new SubCategoryEntity()
            {
                Id = Constants.SubCategoryIdForGetting7,
                Name = Constants.SubCategoryNameForGetting7,
                NormalizedName = Constants.SubCategoryNameForGetting7.ToUpper(),
                NameRu = Constants.SubCategoryNameRuForGetting7,
                NormalizedNameRu = Constants.SubCategoryNameRuForGetting7.ToUpper(),
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedDate = DateTime.Now,
                CategoryId = Constants.CategoryIdForGetting7,
            },
        };
        context.SubCategories.AddRange(subCategories);
    }

    private static void AddCitiesTestData(ApplicationDbContext context)
    {
        var cities = new[]
        {
            // For Update
            new CityEntity()
            {
                Id = Constants.CityIdForUpdate,
                Name = Constants.CityNameForUpdate,
                NormalizedName = Constants.CityNameForUpdate.ToUpper(),
                NameRu = Constants.CityNameRuForUpdate,
                NormalizedNameRu = Constants.CityNameRuForUpdate.ToUpper(),
                CountryId = Constants.CountryIdForUpdate
            },
            // For Update
            new CityEntity()
            {
                Id = Constants.CityIdForDelete,
                Name = Constants.CityNameForDelete,
                NormalizedName = Constants.CityNameForDelete.ToUpper(),
                NameRu = Constants.CityNameRuForDelete,
                NormalizedNameRu = Constants.CityNameRuForDelete.ToUpper(),
                CountryId = Constants.CountryIdForDelete
            },
            new CityEntity()
            {
                Id = Constants.CityIdForGetting1,
                Name = Constants.CityNameForGetting1,
                NormalizedName = Constants.CityNameForGetting1.ToUpper(),
                NameRu = Constants.CityNameRuForGetting1,
                NormalizedNameRu = Constants.CityNameRuForGetting1.ToUpper(),
                CountryId = Constants.CountryIdForGetting1
            },
            new CityEntity()
            {
                Id = Constants.CityIdForGetting2,
                Name = Constants.CityNameForGetting2,
                NormalizedName = Constants.CityNameForGetting2.ToUpper(),
                NameRu = Constants.CityNameRuForGetting2,
                NormalizedNameRu = Constants.CityNameRuForGetting2.ToUpper(),
                CountryId = Constants.CountryIdForGetting2
            },
            new CityEntity()
            {
                Id = Constants.CityIdForGetting3,
                Name = Constants.CityNameForGetting3,
                NormalizedName = Constants.CityNameForGetting3.ToUpper(),
                NameRu = Constants.CityNameRuForGetting3,
                NormalizedNameRu = Constants.CityNameRuForGetting3.ToUpper(),
                CountryId = Constants.CountryIdForGetting3
            },
            new CityEntity()
            {
                Id = Constants.CityIdForGetting4,
                Name = Constants.CityNameForGetting4,
                NormalizedName = Constants.CityNameForGetting4.ToUpper(),
                NameRu = Constants.CityNameRuForGetting4,
                NormalizedNameRu = Constants.CityNameRuForGetting4.ToUpper(),
                CountryId = Constants.CountryIdForGetting4
            },
            new CityEntity()
            {
                Id = Constants.CityIdForGetting5,
                Name = Constants.CityNameForGetting5,
                NormalizedName = Constants.CityNameForGetting5.ToUpper(),
                NameRu = Constants.CityNameRuForGetting5,
                NormalizedNameRu = Constants.CityNameRuForGetting5.ToUpper(),
                CountryId = Constants.CountryIdForGetting5
            },
            new CityEntity()
            {
                Id = Constants.CityIdForGetting6,
                Name = Constants.CityNameForGetting6,
                NormalizedName = Constants.CityNameForGetting6.ToUpper(),
                NameRu = Constants.CityNameRuForGetting6,
                NormalizedNameRu = Constants.CityNameRuForGetting6.ToUpper(),
                CountryId = Constants.CountryIdForGetting6
            }
        };
        
        context.Cities.AddRange(cities);
    }
}