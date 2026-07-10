namespace Qualifier.Application.Cache
{
    public interface IAppCacheService
    {
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? ttl = null);
        void Remove(string key);
    }
}
