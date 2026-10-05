namespace MenuApp.Services;

public interface IRedisService
{
    Task SetValueAsync<T>(string key, T data, TimeSpan? expiry = null);
    Task<T?> GetValueAsync<T>(string key);
    Task DeleteValueAsync(string key);
}