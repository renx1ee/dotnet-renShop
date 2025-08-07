/*using System.Data;
using Dapper;
using Npgsql;

namespace RenStore.Persistence.Test.Common;

public class DatabaseFixture : IAsyncLifetime
{
    public string ConnectionString { get; private set; }

    public DatabaseFixture()
    {
        ConnectionString = "";
    }

    public async Task InitializeAsync()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        
        await connection.ExecuteAsync("IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MyApp_Test') CREATE DATABASE MyApp_Test");

        await InitializeDatabaseSchema(connection);
    }
    
    public Task DisposeAsync()
    {
        throw new NotImplementedException();
    }
}*/