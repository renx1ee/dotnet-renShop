using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RenStore.Application.Common.Exceptions;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.Repository;

public class ColorRepository : IColorRepository
{
    private readonly ApplicationDbContext _context;
    private readonly string _connectionString;
    
    public ColorRepository(
        ApplicationDbContext context,
        string connectionString)
    {
        this._context = context;
        this._connectionString = connectionString;
    }

    public ColorRepository(
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        this._context = context;
        this._connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<int> CreateAsync(Color color, CancellationToken cancellationToken)
    {
        var result = await this._context.Colors.AddAsync(color, cancellationToken);
        await this._context.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }

    public async Task UpdateAsync(Color color, CancellationToken cancellationToken)
    {
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var color = await this.GetByIdAsync(id, cancellationToken);
        this._context.Colors.Remove(color);
        await this._context.SaveChangesAsync(cancellationToken);
    }
    
    // TODO: make ordering by id or name
    public async Task<IEnumerable<Color>> FindAllAsync(CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(this._connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = 
            @"
                SELECT
                    ""color_id"" AS Id,
                    ""color_name"" AS Name,
                    ""normalized_color_name"" AS NormalizedName,
                    ""color_name_ru"" AS NameRu,
                    ""color_code"" AS ColorCode,
                    ""color_description"" AS Description
                FROM
                    ""colors"";
            ";
        
        return await connection.QueryAsync<Color>(sql);
    }

    public async Task<Color?> FindByIdAsync(int id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = 
            @"
                SELECT
                    ""color_id"" AS Id,
                    ""color_name"" AS Name,
                    ""normalized_color_name"" AS NormalizedName,
                    ""color_name_ru"" AS NameRu,
                    ""color_code"" AS ColorCode,
                    ""color_description"" AS Description
                FROM
                    ""colors""
                WHERE
                    ""color_id"" = @Id;
            ";
        
        return await connection
            .QueryFirstOrDefaultAsync<Color>(
                sql, new { Id = id })
                    ?? null;
    }

    public async Task<Color> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await this.FindByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(typeof(Color), id);
    }

    public async Task<Color?> FindByNameAsync(string name, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        
        const string sql = 
            @"
                SELECT
                    ""color_id"" AS Id,
                    ""color_name"" AS Name,
                    ""normalized_color_name"" AS NormalizedName,
                    ""color_name_ru"" AS NameRu,
                    ""color_code"" AS ColorCode,
                    ""color_description"" AS Description
                FROM
                    ""colors""
                WHERE
                    ""normalized_color_name"" = UPPER(@Name);
            ";
        
        return await connection
            .QueryFirstOrDefaultAsync<Color>(
                sql, new { Name = name });
    }

    public async Task<Color> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await this.FindByNameAsync(name, cancellationToken)
            ?? throw new NotFoundException(typeof(Color), name);
    }
}