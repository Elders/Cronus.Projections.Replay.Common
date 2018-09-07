﻿using System.Reflection;
using Elders.Cronus.DomainModeling;
using Elders.Cronus.EventStore;
using Elders.Cronus.Persistence.Cassandra;
using Microsoft.Azure.Documents.Client;
using Elders.Cronus.Persistence.CosmosDb;
using System.Collections.Generic;

namespace Elders.Cronus.Projections.Replay.Common
{
    public static class Player
    {
        public static IEventStorePlayer UseCassandraEventStorePlayer(string boundedContext, List<Assembly> contractsAssembly, string connectionString, bool createKeyspace)
        {
            contractsAssembly.Add(typeof(AggregateCommit).Assembly);
            var eventStoreTableNameStrategy = new TablePerBoundedContext(boundedContext);
            var serializer = new Elders.Cronus.Serialization.NewtonsoftJson.JsonSerializer(contractsAssembly.ToArray());
            var session = CreateSession(connectionString, createKeyspace);
            var player = new CassandraEventStorePlayer(session, eventStoreTableNameStrategy, boundedContext, serializer);
            return player;
        }

        public static IEventStorePlayer UseCosmosEventStorePlayer(Assembly contractsAssembly, DocumentClient client, System.Uri queryUri)
        {
            Assembly[] contractsWithCronus = { contractsAssembly, typeof(AggregateCommit).Assembly };
            var serializer = new Serialization.NewtonsoftJson.JsonSerializer(contractsWithCronus);

            var player = new CosmosEventStorePlayer(client, queryUri, serializer);
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
