using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RenStore.Application.Common.Exceptions;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext context;
    private readonly string? connectionString;
    
    public CategoryRepository(
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        this.context = context;
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<int> CreateAsync(Category category, 
        CancellationToken cancellationToken)
    {
        var result = await this.FindByNameAsync(category.Name, cancellationToken);
        if (result is null)
        {
            await context.Categories.AddAsync(category, cancellationToken);
            await context.SaveChangesAsync();
        }
        return category.Id;
    }

    public async Task UpdateAsync(Category category, 
        CancellationToken cancellationToken)
    {
        Category model = await GetByIdAsync(category.Id, cancellationToken)
            ?? throw new NotFoundException(typeof(Category), category.Id);
            
        context.Categories.Update(category);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, 
        CancellationToken cancellationToken)
    {
        Category category = await GetByIdAsync(id, cancellationToken);
        
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT 
                * 
            FROM 
                ""Categories""";

        return await connection
            .QueryAsync<Category>(
                sql, cancellationToken) 
                    ?? [];
    }
    
    public async Task<Category?> FindByIdAsync(int id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT 
                * 
            FROM 
                ""Categories"" 
            WHERE 
                ""Id""=@Id;";
        
        return await connection
           .QueryFirstOrDefaultAsync<Category>(
               sql, new { Id = id})
                ?? null;
    }
    
    public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await this.FindByIdAsync(id, cancellationToken) 
            ?? throw new NotFoundException(typeof(Category), id);
    }
    
    public async Task<Category?> FindByNameAsync(string name, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT 
                * 
            FROM 
                ""Categories"" 
            WHERE 
                ""Name""=@Name;";
        
        return await connection
            .QueryFirstOrDefaultAsync<Category>(
                sql, new { Name = name }) 
                    ?? null;
    }

    public async Task<Category> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await this.FindByNameAsync(name, cancellationToken) 
            ?? throw new NotFoundException(typeof(Category), name);
    }
}