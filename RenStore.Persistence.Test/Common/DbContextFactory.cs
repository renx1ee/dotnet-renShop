using Microsoft.EntityFrameworkCore;

namespace RenStore.Persistence.Test.Common;

public class DbContextFactory
{
    public static Guid ProductIdForDelete = Guid.NewGuid();
    public static Guid ProductIdForUpdate = Guid.NewGuid();
    
    public static int CategoryIdForUpdate = 42222;
    public static int CategoryIdForDelete = 42324;

    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        var context = new ApplicationDbContext(options);
        context.Database.EnsureCreated();
        
        return context;
    }
        /*context.Products.AddRange(
            #region Products 
            new Product
                {   
                    Id = ProductIdForDelete,
                    ProductName = "productName",
                    Price = 5900,
                    OldPrice = 7900,
                    Discount = 20,
                    Description = "description",
                    InStock = 32,
                    ImagePath = "/images/main/img.png",
                    ImageMiniPath = "/images/main/img.png",
                    ImagesListPath = new List<string>
                    {
                        "/images/main/img_1.png",
                        "/images/main/img_2.png",
                        "/images/main/img_3.png",
                        "/images/main/img_4.png"
                    },
                    Rating = 0,
                    Reviews = null,
                    Category = null,
                    CategoryId = 1,
                    CategoryName = "categoryName",
                    SellerName = "sellerName",
                    Seller = null,
                    SellerId = 1,
                    ProductDetail = new ProductDetail
                    {
                        Article = 0,
                        Brend = "",
                        CountryOfManufacture = "",
                        ModelFeatures = "",
                        DecorativeElements = "",
                        Equipment = "",
                        QuantityPerPackage = 1,
                        Composition = "",
                        Color = ColorStatus.Black,
                        TypeOfPackaging = TypeOfPackaging.Box,
                    },
                    ClothesProduct = new ClothesProduct
                    {
                        Neckline = Neckline.Horseshoe,
                        TheCut = TheCut.Free,
                        TypeOfPockets = TypeOfPockets.None,
                        Gender = Gender.Man,
                        Season = Season.Autumn, 
                        TakingCareOfThings = "wfawfe jljkweaij wea",
                        Sizes =
                        [
                            ClothesSizes.XXXS, 
                            ClothesSizes.XXL, 
                            ClothesSizes.M
                        ]
                    },
                    ShoesProduct = null
                },
            new Product
                {   
                    Id = ProductIdForUpdate,
                    ProductName = "productName",
                    Price = 5900,
                    OldPrice = 7900,
                    Discount = 20,
                    Description = "description",
                    InStock = 32,
                    ImagePath = "/images/main/img.png",
                    ImageMiniPath = "/images/main/img.png",
                    ImagesListPath = new List<string>
                    {
                        "/images/main/img_1.png",
                        "/images/main/img_2.png",
                        "/images/main/img_3.png",
                        "/images/main/img_4.png"
                    },
                    Rating = 0,
                    Reviews = null,
                    Category = null,
                    CategoryId = 1,
                    CategoryName = "categoryName",
                    SellerName = "sellerName",
                    Seller = null,
                    SellerId = 1,
                    ProductDetail = new ProductDetail
                    {
                        Article = 0,
                        Brend = "",
                        CountryOfManufacture = "",
                        ModelFeatures = "",
                        DecorativeElements = "",
                        Equipment = "",
                        QuantityPerPackage = 1,
                        Composition = "",
                        Color = ColorStatus.Black,
                        TypeOfPackaging = TypeOfPackaging.Box,
                    },
                    ClothesProduct = new ClothesProduct
                    {
                        Neckline = Neckline.Horseshoe,
                        TheCut = TheCut.Free,
                        TypeOfPockets = TypeOfPockets.None,
                        Gender = Gender.Man,
                        Season = Season.Autumn, 
                        TakingCareOfThings = "wfawfe jljkweaij wea",
                        Sizes =
                        [
                            ClothesSizes.XXXS, 
                            ClothesSizes.XXL, 
                            ClothesSizes.M
                        ]
                    },
                    ShoesProduct = null
                },
            new Product
                {   
                    Id = Guid.Parse("e090f3f2-ac7e-45a6-9392-995e56564731"),
                    ProductName = "productName",
                    Price = 5900,
                    OldPrice = 7900,
                    Discount = 20,
                    Description = "description",
                    InStock = 32,
                    ImagePath = "/images/main/img.png",
                    ImageMiniPath = "/images/main/img.png",
                    ImagesListPath = new List<string>
                    {
                        "/images/main/img_1.png",
                        "/images/main/img_2.png",
                        "/images/main/img_3.png",
                        "/images/main/img_4.png"
                    },
                    Rating = 0,
                    Reviews = null,
                    Category = null,
                    CategoryId = 1,
                    CategoryName = "categoryName",
                    SellerName = "sellerName",
                    Seller = null,
                    SellerId = 1,
                    ProductDetail = new ProductDetail
                    {
                        Article = 0,
                        Brend = "",
                        CountryOfManufacture = "",
                        ModelFeatures = "",
                        DecorativeElements = "",
                        Equipment = "",
                        QuantityPerPackage = 1,
                        Composition = "",
                        Color = ColorStatus.Black,
                        TypeOfPackaging = TypeOfPackaging.Box,
                    },
                    ClothesProduct = new ClothesProduct
                    {
                        Neckline = Neckline.Horseshoe,
                        TheCut = TheCut.Free,
                        TypeOfPockets = TypeOfPockets.None,
                        Gender = Gender.Man,
                        Season = Season.Autumn, 
                        TakingCareOfThings = "wfawfe jljkweaij wea",
                        Sizes =
                        [
                            ClothesSizes.XXXS, 
                            ClothesSizes.XXL, 
                            ClothesSizes.M
                        ]
                    },
                    ShoesProduct = null
                }
            #endregion
        );
        
        context.Categories.AddRange(
            #region Categories
            new Category()
            {
                Id = CategoryIdForUpdate,
                Name = "Sample Name up",
                Description = "Sample Description",
                ImagePath = "/images/main/img.png",
            }, new Category()
            {
                Id = CategoryIdForDelete,
                Name = "Sample Name del",
                Description = "Sample Description",
                ImagePath = "/images/main/img.png",
            }, new Category()
            {
                Id = 43242,
                Name = "Sample Name",
                Description = "Sample Description",
                ImagePath = "/images/main/img.png",
            }
            #endregion
            );
        
        context.SaveChanges();
        */
    
}