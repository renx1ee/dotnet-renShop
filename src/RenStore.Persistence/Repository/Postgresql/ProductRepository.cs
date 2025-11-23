using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RenStore.Application.Common.Exceptions;
using RenStore.Domain.DTOs;
using RenStore.Domain.DTOs.Product.FullPage;
using RenStore.Domain.Entities;
using RenStore.Domain.Enums.Sorting;
using RenStore.Domain.Repository;

namespace RenStore.Persistence.Repository.Postgresql;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    private readonly string _connectionString;

    private readonly Dictionary<ProductSortBy, string> _sortColumnMapping = new()
    {
        { ProductSortBy.Id, "product_id" }
    };

    public ProductRepository(
        ApplicationDbContext context,
        string connectionString)
    {
        this._context = context;
        this._connectionString = connectionString 
            ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public ProductRepository(
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        this._context = context;
        this._connectionString = configuration
            .GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException(nameof(_connectionString));
    }

    public async Task<Guid> CreateAsync(
        ProductEntity product,
        CancellationToken cancellationToken)
    {
        var result = await _context.Products.AddAsync(product, cancellationToken);
        await this._context.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }
    
    public async Task UpdateAsync(
        ProductEntity product,
        CancellationToken cancellationToken)
    {
        var existingProduct = await this.GetByIdAsync(product.Id, cancellationToken);
        this._context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var existingProduct = await this.GetByIdAsync(id, cancellationToken);
        this._context.Products.Remove(existingProduct);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductEntity>> FindAllAsync(
        CancellationToken cancellationToken,
        uint pageCount = 25,
        uint page = 1,
        bool descending = false,
        ProductSortBy sortBy = ProductSortBy.Id,
        bool? isBlocked = null)
    {
        try
        {
            pageCount = Math.Min(pageCount, 1000);
            uint offset = (page - 1) * pageCount;
            var direction = descending ? "DESC" : "ASC";
            var columnName = _sortColumnMapping.GetValueOrDefault(sortBy, "product_id");
            
            await using var connection = new NpgsqlConnection(this._connectionString);
            await connection.OpenAsync(cancellationToken);

            StringBuilder sql = new StringBuilder(
                $@"
                    SELECT
                        ""product_id"" AS Id,
                        ""is_blocked"" AS IsBlocked,
                        ""overall_rating"" AS OverallRating,
                        ""seller_id"" AS SellerId,
                        ""category_id"" AS CategoryId
                    FROM
                        ""products""
                    
                ");

            if (isBlocked.HasValue)
                sql.Append(@$" WHERE ""is_blocked"" = {isBlocked.Value}");

            sql.Append($" ORDER BY \"{columnName}\" {direction} LIMIT @Count OFFSET @Offset;");

            return await connection
                .QueryAsync<ProductEntity>(
                    sql.ToString(), new
                    {
                        Count = (int)pageCount,
                        Offset = (int)offset
                    });
        }
        catch (PostgresException e)
        {
            throw new Exception($"Database error occured: {e.Message}");
        }
    }
    
    public async Task<ProductEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await using var connection = new NpgsqlConnection(this._connectionString);
            await connection.OpenAsync(cancellationToken);

            const string sql = 
                @"
                    SELECT
                        ""product_id"" AS Id,
                        ""is_blocked"" AS IsBlocked,
                        ""overall_rating"" AS OverallRating,
                        ""seller_id"" AS SellerId,
                        ""category_id"" AS CategoryId
                    FROM
                        ""products""
                    WHERE
                        ""product_id"" = @Id;
                ";

            return await connection
                .QueryFirstOrDefaultAsync<ProductEntity>(
                    sql, new { Id = id });
        }
        catch (PostgresException e)
        {
            throw new Exception($"Database error occured: {e.Message}");
        }
    }

    public async Task<ProductEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await this.FindByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(typeof(ProductEntity), id);
    }

    public async Task<ProductFullDto?> FindFullAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await using var connection = new NpgsqlConnection(this._connectionString);
            await connection.OpenAsync(cancellationToken);

            const string sql =
                @"
                    SELECT 
                        -- products
                        p.""product_id"" AS Id, 
                        p.""is_blocked"" AS IsBlocked, 
                        p.""overall_rating"" AS OverallRating, 
                        p.""seller_id"" AS SellerId, 
                        p.""category_id"" AS CategoryId,
                        -- cloth
                        pc.""product_cloth_id"" AS Id,
                        pc.""gender"" AS Gender,
                        pc.""season"" AS Season,
                        pc.""neckline"" AS Neckline,
                        pc.""the_cut"" AS TheCut,
                        -- cloth sizes
                        pcs.""cloth_size_id"" AS Id,
                        pcs.""amount"" AS Amount,
                        pcs.""cloth_size"" AS ClothesSizes,
                        pcs.""product_cloth_id"" AS ProductClothId,
                        -- price history
                        ph.""price_history_id"" AS Id,
                        ph.""price"" AS Price,
                        ph.""old_price"" AS OldPrice,
                        ph.""discount_price"" AS DiscountPrice,
                        ph.""discount_percent"" AS DiscountPercent,
                        ph.""start_date"" AS StartDate,
                        ph.""end_date"" AS EndDate,
                        ph.""changed_by"" AS ChangedBy,
                        -- variant
                        pv.""product_variant_id"" AS Id,
                        pv.""variant_name"" AS Name,
                        pv.""normalized_variant_name"" AS NormalizedName,
                        pv.""rating"" AS Rating,
                        pv.""article"" AS Article,
                        pv.""in_stock"" AS InStock,
                        pv.""is_available"" AS IsAvailable,
                        pv.""created_date"" AS CreatedDate,
                        pv.""url"" AS Url,
                        pv.""product_id"" AS ProductId,
                        pv.""color_id"" AS ColorId,
                        -- detail
                        pd.""product_detail_id"" AS Id,
                        pd.""description"" AS Description,
                        pd.""model_features"" AS ModelFeatures,
                        pd.""decorative_elements"" AS DecorativeElements,
                        pd.""equipment"" AS Equipment,
                        pd.""composition"" AS Composition,
                        pd.""caring_of_things"" AS CaringOfThings,
                        pd.""type_of_packing"" AS TypeOfPacking,
                        pd.""country_id"" AS CountryOfManufactureId,
                        pd.""product_variant_id"" AS ProductVariantId,
                        -- country
                        ct.""country_name"",
                        -- attribute
                        pa.""attribute_id"" AS Id,
                        pa.""attribute_name"" AS Name,
                        pa.""attribute_value"" AS Value,
                        pa.""product_variant_id"" AS ProductVariantId,
                        -- seller
                        s.""seller_id"" AS Id,
                        s.""seller_name"" AS Name,
                        s.""url"" AS Url,
                        -- seller image
                        ""storage_path"" AS StoragePath,
                        -- product image
                        ""storage_path"" AS StoragePath,
                        ""is_main"" AS IsMain,
                        ""sort_order"" AS SortOrder
                    FROM 
                        ""products"" p
                    INNER JOIN ""product_variants"" pv ON vp.""product_id"" = p.""product_id""
                    LEFT JOIN ""product_clothes"" pc ON pc.""product_id"" = p.""product_id""
                    LEFT JOIN ""product_cloth_sizes"" pcs ON pcs.""product_cloth_id"" = pc.""product_cloth_id""
                    LEFT JOIN ""product_price_histories"" ph ON ph.""product_variant_id"" = pv.""product_variant_id""
                    LEFT JOIN ""product_details"" pd ON pd.""product_variant_id"" = pv.""product_variant_id""
                    LEFT JOIN ""product_attributes"" pa ON pa.""product_variant_id"" = pv.""product_variant_id""
                    LEFT JOIN ""sellers"" s ON s.""seller_id"" = p.""seller_id""
                    LEFT JOIN ""seller_images"" si ON si.""seller_id"" = s.""seller_id""
                    LEFT JOIN ""product_images"" pi ON pi.""product_id"" = p.""product_id""
                    LEFT JOIN ""countries"" ct ON ct.""country_id"" = pd.""country_id"" 
                    WHERE 
                        p.""product_id"" = @Id
                    ORDER BY 
                        -- product images
                        pi.""is_main"" DESC,
                        pi.""sort_order"" ASC
                        -- product cloth sizes
                        pcs.""amount"" ASC,
                        -- price history
                        ph.""start_date"" ASC;
                ";

            var lookupPrice = new Dictionary<Guid, ProductPriceHistoryDto>(); 
            var lookupSize = new Dictionary<Guid, ProductClothSizeDto>(); 
            var lookupAttributes = new Dictionary<Guid, ProductAttributeDto>(); 
            var lookupProductImages = new Dictionary<Guid, ProductImageDto>(); 

            var result = await connection.QueryAsync<
                ProductFullDto>(
                sql, 
                (
                    ProductDto product, 
                    ProductVariantDto variant, 
                    ProductDetailDto detail, 
                    ProductClothDto cloth, 
                    SellerDto seller, 
                    SellerImageDto sellerImage, 
                    ProductClothSizeDto clothSize, 
                    ProductAttributeDto attribute, 
                    ProductPriceHistoryDto priceHistory, 
                    ProductImageDto productImage ) =>
                {
                    if (!lookupPrice!.ContainsKey(priceHistory.Id))
                        lookupPrice[priceHistory.Id] = priceHistory;
                    
                    if (!lookupSize!.ContainsKey(clothSize.Id))
                        lookupSize[clothSize.Id] = clothSize;
                    
                    if (!lookupAttributes!.ContainsKey(attribute.Id))
                        lookupAttributes[attribute.Id] = attribute;
                    
                    if (!lookupProductImages!.ContainsKey(productImage.Id))
                        lookupProductImages[productImage.Id] = productImage;
                    
                    return new ProductFullDto(
                        Product: product,
                        Variant: variant,
                        Detail: detail,
                        Cloth: cloth,
                        Seller: seller,
                        SellerImage: sellerImage,
                        ClothSize: lookupSize.Values.ToList(),
                        Attributes: lookupAttributes.Values.ToList(),
                        Prices: lookupPrice.Values.ToList(),
                        ProductImages: lookupProductImages.Values.ToList()
                    );
                });

            return result.FirstOrDefault();
        }
        catch (PostgresException e)
        {
            throw new Exception($"Database error occured: {e.Message}");
        }
    }

    public async Task<ProductFullDto?> GetFullAsync(Guid id, CancellationToken cancellationToken)
    {
        return await this.FindFullAsync(id, cancellationToken)
            ?? throw new NotFoundException(typeof(ProductFullDto), id);
    }
}