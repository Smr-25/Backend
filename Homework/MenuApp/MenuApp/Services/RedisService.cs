using System.Text.Json;
using StackExchange.Redis;

namespace MenuApp.Services;

public class RedisService(IConnectionMultiplexer connectionMultiplexer) : IRedisService
{
    private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };
    

    public async Task SetValueAsync<T>(string key, T data, TimeSpan? expiry = null)
    {
        var jsonData = JsonSerializer.Serialize(data, _jsonOptions);
        if (expiry.HasValue)
            await _database.StringSetAsync(key, jsonData, expiry.Value);
        else
            await _database.StringSetAsync(key, jsonData);
    }
    
    public async Task<T?> GetValueAsync<T>(string key)
    {
        var jsonData = await _database.StringGetAsync(key);
        if (jsonData.IsNullOrEmpty)
        {
            return default;
        }
        return JsonSerializer.Deserialize<T>(jsonData.ToString(), _jsonOptions);
    }
    
    public  async Task DeleteValueAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}