using System;
using System.Net;
using Framework.Core.Helpers;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Framework.Data.CacheProviders.Redis
{
    public class RedisConnectionWrapper
    {
        private readonly ILogger _logger;
        private ConnectionMultiplexer _connection;
        private readonly ConfigurationOptions _options;

        private readonly object _lock = new object();

        public RedisConnectionWrapper(ConfigurationOptions options, ILogger<RedisConnectionWrapper> logger)
        {
            _options = options;
            _logger = logger;
        }

        private ConnectionMultiplexer GetConnection()
        {
            if (_connection != null && _connection.IsConnected) return _connection;

            lock (_lock)
            {
                if (_connection != null && _connection.IsConnected) return _connection;

                if (_connection != null)
                {
                    _logger.LogDebug("Connection disconnected. Disposing connection...");
                    _connection.Dispose();
                }

                _logger.LogDebug("Creating new instance of Redis Connection");
                var lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
                {
                    return ConnectionMultiplexer.Connect(_options);
                });

                _connection = lazyConnection.Value;
            }

            return _connection;
        }

        public IDatabase Database(int? db = null)
        {
            return GetConnection().GetDatabase(db ?? _options.DefaultDatabase.Value);
        }

        public IServer Server(EndPoint endPoint)
        {
            return GetConnection().GetServer(endPoint);
        }

        public EndPoint[] GetEndpoints()
        {
            return GetConnection().GetEndPoints();
        }

        public void FlushDb(int? db = null)
        {
            var endPoints = GetEndpoints();

            foreach (var endPoint in endPoints)
            {
                Server(endPoint).FlushDatabase(db ?? _options.DefaultDatabase.Value);
            }
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}