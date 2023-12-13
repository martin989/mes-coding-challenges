namespace config_api.Repositories;

using Dapper;
using config_api.Entities;
using config_api.Helpers;

public interface IDataRepository
{
    Task<IEnumerable<Data>> GetAll();
    Task<Data> GetByKey(string key);
    Task Create(Data data);
    Task Update(Data data);
    Task Delete(string key);
    Task<IEnumerable<Data>> MatchFilter(string name, string value);
}

public class DataRepository : IDataRepository
{
    private DataContext _context;

    public DataRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Data>> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = @"
            SELECT * FROM Data
        ";
        return await connection.QueryAsync<Data>(sql);
    }

    public async Task<Data> GetByKey(string key)
    {
        using var connection = _context.CreateConnection();
        var sql = @"
            SELECT * FROM Data 
            WHERE Key = @key
        ";
        return await connection.QuerySingleOrDefaultAsync<Data>(sql, new { key });
    }

    public async Task Create(Data data)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            INSERT INTO Data (Key, Value)
            VALUES (@Key, @Value)
        """;
        await connection.ExecuteAsync(sql, data);
    }

    public async Task Update(Data data)
    {
        using var connection = _context.CreateConnection();
        var sql = @"
            UPDATE Data 
            SET Value = @Value
            WHERE Key = @Key
        ";
        await connection.ExecuteAsync(sql, data);
    }

    public async Task Delete(string key)
    {
        using var connection = _context.CreateConnection();
        var sql = @"
            DELETE FROM Data 
            WHERE Key = @key
        ";
        await connection.ExecuteAsync(sql, new { key });
    }

    public async Task<IEnumerable<Data>> MatchFilter(string name, string value)
    {
        using var connection = _context.CreateConnection();

        var sql = @"SELECT DISTINCT Data.Key, Data.Value FROM Data,json_tree ( Data.Value ) as newData  WHERE newData.key like @name and newData.value like @value";
        return await connection.QueryAsync<Data>(sql, new { name, value });
    }
}