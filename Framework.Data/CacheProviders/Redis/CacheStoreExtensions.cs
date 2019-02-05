using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Framework.Data.CacheProviders.Redis
{
    public static class CacheStoreExtensions
    {
        public static T Get<T>(this IDistributedCache source, string key) where T : class
        {
            var data = source.Get(key);
            var stringData = Encoding.UTF8.GetString(data);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(stringData);
        }

        public static async Task<T> GetAsync<T>(this IDistributedCache source, string key) where T : class
        {
            var data = await source.GetAsync(key);
            var stringData = Encoding.UTF8.GetString(data);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(stringData);
        }

        public static T Get<T>(this IDistributedCache source, string key, TimeSpan time, Func<T> fetch) where T : class
        {
            if (source.Exists(key))
                return source.Get<T>(key);

            var result = fetch();

            if (result != null)
            {
                var stringToPersist = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                var data = Encoding.UTF8.GetBytes(stringToPersist);

                var options = new DistributedCacheEntryOptions().SetSlidingExpiration(time);
                source.Set(key, data, options);
            }

            return result;
        }

        public static async Task<T> GetAsync<T>(this IDistributedCache source, string key, TimeSpan time, Func<Task<T>> fetch) where T : class
        {

            if (await source.ExistsAsync(key))
                return await source.GetAsync<T>(key);

            var result = await fetch();

            if (result != null)
            {
                var stringToPersist = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                var data = Encoding.UTF8.GetBytes(stringToPersist);

                var options = new DistributedCacheEntryOptions().SetSlidingExpiration(time);
                source.Set(key, data, options);

            }


            return result;
        }

        public static bool Exists(this IDistributedCache source, string key) => source.Get(key) != null;
        public static async Task<bool> ExistsAsync(this IDistributedCache source, string key) => await source.GetAsync(key) != null;

    }
}