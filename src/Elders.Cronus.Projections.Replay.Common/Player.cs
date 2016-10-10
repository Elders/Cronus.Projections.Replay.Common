using System.Reflection;
using Elders.Cronus.DomainModeling;
using Elders.Cronus.EventStore;
using Elders.Cronus.Persistence.Cassandra;

namespace Elders.Cronus.Projections.Replay.Common
{
    public static class Player
    {
        public static IEventStorePlayer UseCassandraEventStorePlayer(Assembly contractsAssembly, string connectionString, bool createKeyspace)
        {
            Assembly[] contractsWithCronus = { contractsAssembly, typeof(AggregateCommit).Assembly };
            var boundedContext = contractsAssembly.GetBoundedContext().BoundedContextName;
            var eventStoreTableNameStrategy = new TablePerBoundedContext(contractsAssembly);
            var serializer = new Elders.Cronus.Serialization.NewtonsoftJson.JsonSerializer(contractsWithCronus);
            var session = CreateSession(connectionString, createKeyspace);
            var player = new CassandraEventStorePlayer(session, eventStoreTableNameStrategy, boundedContext, serializer);
            return player;
        }

        static Cassandra.ISession CreateSession(string connectionString, bool createKeyspace)
        {
            var settings = new Cassandra.CassandraConnectionStringBuilder(connectionString);
            var cluster = Cassandra.Cluster.Builder()
                .AddContactPoints(settings.ContactPoints)
                .WithPort(settings.Port)
                .Build();

            var session = cluster.Connect();

            if (createKeyspace)
                session.CreateKeyspaceIfNotExists(settings.DefaultKeyspace);

            session.ChangeKeyspace(settings.DefaultKeyspace);
            return session;
        }
    }
}
