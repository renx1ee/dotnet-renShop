using System.Data;
using Npgsql;

namespace RenStore.Persistence.Test.Common;

public class DataBaseFixture : IDisposable
{
    private IDbConnection DbConnection { get; }
    
    public DataBaseFixture()
    {
        DbConnection = new NpgsqlConnection();
        DbConnection.Open();
    }
    
    public void Dispose()
    {
        DbConnection.Dispose();
    }
}