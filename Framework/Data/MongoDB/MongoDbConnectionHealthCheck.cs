using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver.Core.Clusters;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Data.MongoDB
{
    public sealed class MongoDbConnectionHealthCheck : IHealthCheck
    {
        public static readonly string HealthCheckName = "mongodb_connection";

        /// <summary>
        /// Conexão com o banco.
        /// </summary>
        private readonly MongoDBConnectionWraper _connection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public MongoDbConnectionHealthCheck(MongoDBConnectionWraper connection)
        {
            _connection = connection;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var databases = await _connection.MongoClient.ListDatabasesAsync();
                await databases.MoveNextAsync(); // Force MongoDB to connect to the database.

                if (_connection.MongoClient.Cluster.Description.State != ClusterState.Connected)
                    return new HealthCheckResult(status: context.Registration.FailureStatus);
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
            }

            return HealthCheckResult.Healthy();
        }
    }
}
