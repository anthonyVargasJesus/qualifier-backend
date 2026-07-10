using Microsoft.Extensions.Caching.Memory;

namespace Qualifier.Application.Cache
{
    // Caché en proceso (IMemoryCache) para datos de catálogo/configuración que casi nunca
    // cambian (niveles de madurez, evaluación actual, catálogo de requisitos/controles por
    // norma) y que hoy se vuelven a calcular en cada llamada a los endpoints "bootstrap".
    // Los commands que escriben esas tablas invalidan la clave correspondiente explícitamente;
    // el TTL de acá abajo es solo una red de seguridad por si algún camino de invalidación se
    // escapa, no el mecanismo principal.
    public class AppCacheService : IAppCacheService
    {
        private static readonly TimeSpan DefaultTtl = TimeSpan.FromMinutes(30);

        private readonly IMemoryCache _memoryCache;

        public AppCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? ttl = null)
        {
            if (_memoryCache.TryGetValue(key, out T? cached) && cached != null)
                return cached;

            var value = await factory();
            _memoryCache.Set(key, value, ttl ?? DefaultTtl);
            return value;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
