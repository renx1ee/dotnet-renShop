using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RenStore.Application.Common.Exceptions;
using RenStore.Domain.Entities;
using RenStore.Persistence.SortedEnums;

namespace RenStore.Persistence.Repository.Postgresql;

public class CountryRepository
{
    private readonly ApplicationDbContext _context;
    private readonly string _connectionString;

    private readonly Dictionary<CountrySortBy, string> _sortColumnMapping =
        new ()
        {
            { CountrySortBy.Id, "country_id" },
            { CountrySortBy.Name, "country_name" }
        };

    public CountryRepository(
        ApplicationDbContext context,
        string connectionString)
    {
        this._context = context;
        this._connectionString = connectionString 
            ?? throw new ArgumentNullException(nameof(connectionString));;
    }
    
    public CountryRepository(
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        this._context = context;
        this._connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<int> CreateAsync(
        CountryEntity country, 
        CancellationToken cancellationToken)
    {
        var result = await _context.Countries.AddAsync(country, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }
    
    public async Task UpdateAsync(
        CountryEntity country, 
        CancellationToken cancellationToken)
    {
        _context.Countries.Update(country);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(
        int id, 
        CancellationToken cancellationToken)
    {
        var country = await this.GetByIdAsync(id, cancellationToken);
        _context.Countries.Remove(country);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<CountryEntity?>> FindAllAsync(
        CancellationToken cancellationToken,
        CountrySortBy sortBy = CountrySortBy.Id,
        uint count = 25,
        bool descending = false)
    {
        try
        {
            await using var connection = new NpgsqlConnection(this._connectionString);
            await connection.OpenAsync(cancellationToken);

            count = Math.Min(count, 1000);
            string columnName = _sortColumnMapping.GetValueOrDefault(sortBy, "country_id");
            var direction = descending ? "DESC" : "ASC";

            string sql =
                $@"
                    SELECT
                        ""country_id"" AS Id,
                        ""country_name"" AS Name,
                        ""normalized_country_name"" AS NormalizedName,
                        ""country_code"" AS Code
                    FROM
                        ""countries""
                    ORDER BY 
                        {columnName} {direction}
                    LIMIT @Count;
                ";

            return await connection
                .QueryAsync<CountryEntity?>(
                    sql, new
                    {
                        Count = (int)count,
                    });
        }
        catch (PostgresException e)
        {
            throw new Exception($"Database error occured: {e.Message}"); 
        }
    }

    public async Task<CountryEntity?> FindByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        try
        {
            await using var connection = new NpgsqlConnection(this._connectionString);
            await connection.OpenAsync(cancellationToken);

            const string sql =
                @"
                    SELECT
                        ""country_id"" AS Id,
                        ""country_name"" AS Name,
                        ""normalized_country_name"" AS NormalizedName,
                        ""country_code"" AS Code
                    FROM
                        ""countries""
                    WHERE
                        ""country_id"" = @Id;
                ";
            
            return await connection
                .QueryFirstOrDefaultAsync<CountryEntity>(
                    sql, new { Id = id });   
        }
        catch (PostgresException e)
        {
            throw new Exception($"Database error occured: {e.Message}");
        }
    }
    
    public async Task<CountryEntity?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await this.FindByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(typeof(CountryEntity), id);
    }

    public async Task<IEnumerable<CountryEntity?>> FindByNameAsync(
        string name,
        CancellationToken cancellationToken,
        CountrySortBy sortBy = CountrySortBy.Name,
        uint count = 25,
        bool descending = false)
    {
        try
        {
            await using var connection = new NpgsqlConnection(this._connectionString);
            await connection.OpenAsync(cancellationToken);
            
            count = Math.Min(count, 1000);
            string columnName = _sortColumnMapping.GetValueOrDefault(sortBy, "country_id");
            var direction = descending ? "DESC" : "ASC";

            string sql =
                @$"
                    SELECT
                        ""country_id"" AS Id,
                        ""country_name"" AS Name,
                        ""normalized_country_name"" AS NormalizedName,
                        ""country_code"" AS Code
                    FROM
                        ""countries""
                    WHERE
                        ""normalized_country_name"" 
                            LIKE @Name
                    ORDER BY 
                        {columnName} {direction}
                    LIMIT @Count;
                ";
            
            return await connection
                .QueryAsync<CountryEntity?>(
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

    public async Task<IEnumerable<CountryEntity?>> GetByNameAsync(
        string name,
        CancellationToken cancellationToken,
        CountrySortBy sortBy = CountrySortBy.Name,
        uint count = 25,
        bool descending = false)
    {
        return await this.FindByNameAsync(name, cancellationToken, sortBy, count, descending)
            ?? throw new NotFoundException(typeof(CountryEntity), name);
    }
}