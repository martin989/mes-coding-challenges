namespace config_api.Helpers;

using System.Data;
using System.Text.Json;
using config_api.Entities;
using Dapper;
using Microsoft.Data.Sqlite;

public class DataContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(Configuration.GetConnectionString("WebApiDatabase"));
    }

    public async Task Init()
    {
        using var connection = CreateConnection();
        await _initData();
        async Task _initData()
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS 
                Data (
                    	Key TEXT NOT NULL UNIQUE,
                        Value nvarchar(4000) NOT NULL
                );
            """;
            await connection.ExecuteAsync(sql);
        }

    }
}