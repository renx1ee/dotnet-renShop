using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RenStore.Application.Common.Exceptions;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.Repository;

public class SellerRepository : ISellerRepository
{
    private readonly ApplicationDbContext context;
    private readonly string? connectionString;
    
    public SellerRepository(
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
        this.context = context;
    }
    
    public SellerRepository(
        ApplicationDbContext context,
        string connectionString)
    {
        this.connectionString = connectionString;
        this.context = context;
    }

    public async Task<int> CreateAsync(Seller seller, CancellationToken cancellationToken)
    {
        await context.Sellers.AddAsync(seller, cancellationToken);
        await context.SaveChangesAsync();
        return seller.Id;
    }

    public async Task UpdateAsync(Seller seller, CancellationToken cancellationToken)
    {
        Seller model = await GetByIdAsync(seller.Id, cancellationToken)
            ?? throw new NotFoundException(typeof(Seller), seller.Id);
        
        context.Sellers.Update(seller);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var seller = await context.Sellers
            .FirstOrDefaultAsync(s => 
                s.Id == id, 
                cancellationToken)
            ?? throw new NotFoundException(typeof(Category), id);
        
        context.Sellers.Remove(seller);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Seller>?> GetAllAsync(CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""Sellers""";
        
        return await connection
            .QueryAsync<Seller>(
                sql, cancellationToken)
                    ?? null;
    }
    
    public async Task<Seller> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await this.FindByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(typeof(Seller), id);
    }

    public async Task<Seller?> FindByIdAsync(int id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""Sellers""
            WHERE
                ""Id""=@Id";
        
        return await connection
            .QueryFirstOrDefaultAsync<Seller>(
                sql, new { Id = id })
                    ?? null;
    }

    public async Task<Seller?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await this.FindByNameAsync(name, cancellationToken)
            ?? throw new NotFoundException(typeof(Seller), name);
    }
    
    public async Task<Seller?> FindByNameAsync(string name, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""Sellers""
            WHERE
                ""Name""=@Name";
        
        return await connection
           .QueryFirstOrDefaultAsync<Seller>(
               sql, new { Name = name })
                   ?? null;
    }
}