using RenStore.Domain.Entities;
using RenStore.Domain.Entities.Products;

namespace RenStore.Application.Repository;
/// <summary>
/// Repository for working with entity Product.
/// Provides basic CRUD operations and methods for working with data.
/// </summary>
/// <remarks>
/// Initializes a new repository instance.
/// </remarks>
/// <param name="context">Database context.</param>
/// <param name="connectionString">Database connection string.</param>
public interface IProductRepository 
{
    /// <summary>
    /// Create a new Product.
    /// </summary>
    /// <param name="product">Product Model for create.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Product ID if Product is created.</returns>
    Task<Guid> CreateAsync(Product product, CancellationToken cancellationToken);
    /// <summary>
    /// Product Update.
    /// </summary>
    /// <param name="product">Product Model for update.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the Product is not found.</exception>
    Task UpdateAsync(Product product, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes a Product by ID.
    /// </summary>
    /// <param name="id">Product ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the Product is not found.</exception>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Get all Products.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return a IEnumerable collection of Products.</returns>
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
    /// <summary>
    /// Finds a Product by Product ID.
    /// </summary>
    /// <param name="id">Category ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Category if category is found else returns null.</returns>
    Task<Product?> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Product by Product ID.
    /// </summary>
    /// <param name="id">Category ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Category if category is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the category is not found.</exception>
    Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Clothes Product by Product ID.
    /// </summary>
    /// <param name="id">Product ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Product if Product is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Product is not found.</exception>
    Task<ClothesProduct?> GetClothesByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Shoes Product by Product ID.
    /// </summary>
    /// <param name="id">Product ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Product if Product is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Product is not found.</exception>
    Task<ShoesProduct?> GetShoesByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a Product details by Product ID.
    /// </summary>
    /// <param name="id">Product ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Product details if Product is found else returns null.</returns>
    Task<ProductDetail?> FindDetailsByProductIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Product details by Product ID.
    /// </summary>
    /// <param name="id">Product ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Product details if Product is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Product is not found.</exception>
    Task<ProductDetail?> GetDetailsByProductIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a Products by Category ID.
    /// </summary>
    /// <param name="categoryId">Category ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return IEnumerable collection of Products if Products is found else returns an Empty collection.</returns>
    Task<IEnumerable<Product>?> FindByCategoryIdAsync(int categoryId, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Products by Category ID.
    /// </summary>
    /// <param name="categoryId">Category ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return IEnumerable collection of Products if Products is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Product is not found.</exception>
    Task<IEnumerable<Product?>> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a ProductDetail by article.
    /// </summary>
    /// <param name="article">Article.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductDetail if Product is found else returns null.</returns>
    Task<ProductDetail?> FindDetailByArticleIdAsync(int article, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a Products by price.
    /// </summary>
    /// <param name="maxPrice">Max Price.</param>
    /// <param name="minPrice">Min Price.</param>
    /// <param name="descending">Descending.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Products if Products is found else returns en empty collection.</returns>
    Task<IEnumerable<Product>> FindSortedByPriceAsync(bool descending, CancellationToken cancellationToken, decimal? maxPrice = null, decimal? minPrice = null);
    /// <summary>
    /// Finds a Products by rating.
    /// </summary>
    /// <param name="descending">Descending.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Products if Products is found else returns en empty collection.</returns>
    Task<IEnumerable<Product>> FindSortedByRatingAsync(bool descending, CancellationToken cancellationToken);
    /// <summary>
    /// Search a Products by Product Name and Description.
    /// </summary>
    /// <param name="keyWord">The Words that will be searched for.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Products if Products is found else returns en empty collection.</returns>
    Task<IEnumerable<Product>> SearchByNameAsync(string keyWord, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a Products by SellerId.
    /// </summary>
    /// <param name="sellerId">Seller Id.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Products if Products is found else returns en empty collection.</returns>
    Task<IEnumerable<Product>> FindBySellerIdAsync(int sellerId, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a Products by CreatedDate.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <param name="descending">Descending.</param>
    /// <returns>Return Products if Products is found else returns en empty collection.</returns>
    Task<IEnumerable<Product>> FindSortedByNoveltyAsync(bool descending, CancellationToken cancellationToken);
}