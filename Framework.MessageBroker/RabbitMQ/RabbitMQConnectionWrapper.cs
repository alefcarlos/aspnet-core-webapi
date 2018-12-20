using System;
using Framework.Core.Helpers;
using RabbitMQ.Client;

namespace Framework.MessageBroker.RabbitMQ
{
    public class RabbitMQConnectionWrapper : IDisposable
    {
        public IConnection Connection { get; private set; }

        private bool disposed;

        public RabbitMQConnectionWrapper(string appName)
        {
            disposed = false;
            var uri = CommonHelpers.GetValueFromEnv<string>("RABBITMQ_URI");

            var factory = new ConnectionFactory();
            factory.Uri = new Uri(uri);
            factory.AutomaticRecoveryEnabled = true;
            Connection = factory.CreateConnection(appName);
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