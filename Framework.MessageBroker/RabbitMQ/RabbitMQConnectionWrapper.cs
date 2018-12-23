using System;
using Framework.Core.Helpers;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Framework.MessageBroker.RabbitMQ
{
    public class RabbitMQConnectionWrapper : IDisposable
    {
        public IConnection Connection { get; private set; }

        private readonly string _appName;

        private bool disposed;
        private ConnectionFactory factory = new ConnectionFactory();

        public RabbitMQConnectionWrapper(string appName)
        {
            disposed = false;
            _appName = appName;

            var uri = CommonHelpers.GetValueFromEnv<string>("RABBITMQ_URI");

            factory.Uri = new Uri(uri);
            factory.AutomaticRecoveryEnabled = true;
            factory.RequestedHeartbeat = 60;

            // _logger = logger;
            TryConnect();
        }

        private void TryConnect()
        {
            Connection = factory.CreateConnection(_appName);
            WatchConnectionHealth();
        }

        /// <summary>
        /// 
        /// </summary>
        private void WatchConnectionHealth()
        {
            Connection.ConnectionShutdown += (sender, e) =>
            {
                if (disposed) return;
                Console.WriteLine("A RabbitMQ connection is shutdown. Trying to re-connect...");
                TryConnect();
            };

            Connection.CallbackException += (sender, e) =>
            {
                if (disposed) return;
                Console.WriteLine("A RabbitMQ connection throw exception. Trying to re-connect...");
                TryConnect();
            };

            Connection.ConnectionBlocked += (sender, e) =>
            {
                if (disposed) return;
                Console.WriteLine("A RabbitMQ connection is blocked. Trying to re-connect...");
                TryConnect();
            };
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Connection?.Close();
                    Connection?.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}