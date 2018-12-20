using System;
using System.Threading.Tasks;

namespace Framework.Data.CacheProviders
{
    public static class CacheStoreExtensions
    {
        public static T Get<T>(this ICacheProvider source, string key, TimeSpan time, Func<T> fetch) where T : class
        {
            if (source.Exists(key))
                return source.Get<T>(key);

            var result = fetch();

            if (result != null)
            {
                source.Set(key, result, time);
            }

            return result;
        }

        public static async Task<T> GetAsync<T>(this ICacheProvider source, string key, TimeSpan time, Func<Task<T>> fetch) where T : class
        {

            if (await source.ExistsAsync(key))
                return await source.GetAsync<T>(key);

            var result = await fetch();

            if (result != null)
            {
                await source.SetAsync(key, result, time);
            }


            return result;
        }
    }
}