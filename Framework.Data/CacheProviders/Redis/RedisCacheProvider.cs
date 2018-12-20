using System;
using System.Threading.Tasks;
using Framework.Core.Serializer;
using StackExchange.Redis;

namespace Framework.Data.CacheProviders.Redis
{
    public class RedisCacheProvider : IRedisCacheProvider
    {
        private readonly IDatabase _database;
        private readonly JsonSerializerCommon _serializer;
        // private readonly CommandFlags _readFlag;

        public RedisCacheProvider(RedisConnectionWrapper connectionWrapper, ConfigurationOptions settings, JsonSerializerCommon serializer)
        {
            _database = connectionWrapper.Database(settings.DefaultDatabase);
            _serializer = serializer;
            // _readFlag = settings. ? CommandFlags.PreferSlave : CommandFlags.PreferMaster;
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var result = await _database.StringGetAsync(key);

            return _serializer.Deserialize<T>(result);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiredIn)
        {
            await _database.StringSetAsync(key, _serializer.Serialize(value), expiredIn);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public bool Exists(string key)
        {
            return _database.KeyExists(key);
        }

        public T Get<T>(string key)
        {
            return _serializer.Deserialize<T>(_database.StringGet(key));
        }

        public void Set<T>(string key, T value, TimeSpan expiredIn)
        {
            _database.StringSet(key, _serializer.Serialize(value), expiredIn);
        }

        public void Remove(string key)
        {
            _database.KeyDelete(key);
        }
    }
}