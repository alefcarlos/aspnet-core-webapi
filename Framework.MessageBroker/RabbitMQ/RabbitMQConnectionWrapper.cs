using Framework.Core.Helpers;
using RabbitMQ.Client;
using System;

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

            Connection = factory.CreateConnection(_appName);
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