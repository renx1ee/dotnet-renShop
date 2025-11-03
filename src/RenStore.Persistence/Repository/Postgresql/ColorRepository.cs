using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RenStore.Application.Common.Exceptions;
using RenStore.Domain.Entities;
using RenStore.Persistence.SortedEnums;

namespace RenStore.Persistence.Repository.Postgresql;

public class ColorRepository /* : IColorRepository*/
{
    // TODO: OFFSET
    private readonly ApplicationDbContext _context;
    private readonly string _connectionString;
    private readonly Dictionary<ColorSortBy, string> _sortColumnMapping =
        new()
        {
            { ColorSortBy.Id, "color_id" },
            { ColorSortBy.NormalizedName, "normalized_color_name" }
        };
    
    public ColorRepository(
        ApplicationDbContext context,
        string connectionString)
    {
        this._context = context;
        this._connectionString = connectionString  ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public ColorRepository(
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        this._context = context;
        this._connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<int> CreateAsync(ColorEntity color, CancellationToken cancellationToken)
    {
        var result = await this._context.Colors.AddAsync(color, cancellationToken);
        await this._context.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }

    public async Task UpdateAsync(ColorEntity color, CancellationToken cancellationToken)
    {
        var existingColor = await this.GetByIdAsync(color.Id, cancellationToken);
        
        _context.Colors.Update(color);
        await this._context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var color = await this.GetByIdAsync(id, cancellationToken);
        this._context.Colors.Remove(color);
        await this._context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<ColorEntity>> FindAllAsync(
        CancellationToken cancellationToken,
        ColorSortBy sortBy = ColorSortBy.Id,
        uint count = 25,
        bool descending = false)
    {
        try
        {
            await using var connection = new NpgsqlConnection(this._connectionString);
            await connection.OpenAsync(cancellationToken);

            count = Math.Min(count, 1000);
            var direction = descending ? "DESC" : "ASC";
            string columnName = _sortColumnMapping.GetValueOrDefault(sortBy, "color_id");
        
            string sql =
                $@"
                    SELECT
                        ""color_id"" AS Id,
                        ""color_name"" AS Name,
                        ""normalized_color_name"" AS NormalizedName,
                        ""color_name_ru"" AS NameRu,
                        ""color_code"" AS ColorCode,
                        ""color_description"" AS Description
                    FROM
                        ""colors"" 
                    ORDER BY {columnName} {direction} 
                    LIMIT @Count;
                ";
        
            return await connection
                .QueryAsync<ColorEntity>(
                    sql, new
                    {
                        Count = (int)count
                    });
        }
        catch (PostgresException e)
        {
            throw new Exception($"Database error occured: {e.Message}");
        }
    }

    public async Task<ColorEntity?> FindByIdAsync(
        int id, 
        CancellationToken cancellationToken)
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            string sql =  
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
               .QueryFirstOrDefaultAsync<ColorEntity>(
                   sql, new { Id = id })
                       ?? null;
        }
        catch (PostgresException e)
        {
            throw new Exception($"Database error occured: {e.Message}");
        }
    }

    public async Task<ColorEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await this.FindByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(typeof(ColorEntity), id);
    }
    
    public async Task<IEnumerable<ColorEntity?>> FindByNameAsync(
        string name, 
        CancellationToken cancellationToken,
        ColorSortBy sortBy = ColorSortBy.Id,
        uint count = 25,
        bool descending = false)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(nameof(name));

        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            count = Math.Min(count, 1000);
            var direction = descending ? "DESC" : "ASC";
            string columnName = _sortColumnMapping.GetValueOrDefault(sortBy, "color_id");
        
            string sql = 
                $@"
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
                    ""normalized_color_name"" 
                        LIKE @Name
                ORDER BY {columnName} {direction} 
                LIMIT @Count;
            ";
        
            return await connection
                .QueryAsync<ColorEntity>(
                    sql, new
                    {
                        Name = $"%{name.ToUpper()}%",
                        Count = (int)count
                    });
        }
        catch (PostgresException e)
        {
            throw new Exception($"Database error occured: {e.Message}");
        }
    }

    public async Task<IEnumerable<ColorEntity?>> GetByNameAsync(
        string name, 
        CancellationToken cancellationToken,
        ColorSortBy sortBy = ColorSortBy.Id,
        uint count = 25,
        bool descending = false)
    {
        var result = await this.FindByNameAsync(name, cancellationToken, sortBy, count, descending);
        
        if (result is null || !result.Any())
            throw new NotFoundException(typeof(ColorEntity), name);
        
        return result;
    }
}