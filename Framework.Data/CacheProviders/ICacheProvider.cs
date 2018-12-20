//Based on: https://ruhul.wordpress.com/2014/07/23/use-redis-as-cache-provider/
using System;
using System.Threading.Tasks;

namespace Framework.Data.CacheProviders
{
    public interface ICacheProvider
    {
        bool Exists(string key);
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan expiredIn);
        void Remove(string key);

        Task<bool> ExistsAsync(string key);
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan expiredIn);
        Task RemoveAsync(string key);
    }
}