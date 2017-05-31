using System;
using System.Collections.Generic;
using System.Linq;
using Elders.Cronus.DomainModeling;
using Elders.Cronus.EventStore;
using Elders.Cronus.Projections.Replay.Common.Extensions;

namespace Elders.Cronus.Projections.Replay.Common.ReplayDefinition
{
    public abstract class ReplayDefinition : IObserver<AggregateCommit>
    {
        List<IProjectionWithEvents> registeredProjections;

        List<IProjectionWithEvents> availableForReplayProjections;

        /// <summary>
        /// All projections must be passed initially
        /// </summary>
        /// <param name="projections"></param>
        public ReplayDefinition(IEnumerable<IProjection> projections)
        {
            registeredProjections = new List<IProjectionWithEvents>();
            availableForReplayProjections = new List<IProjectionWithEvents>();

            foreach (var projection in projections)
            {
                var projectionWithEvents = new ProjectionWithEvents(projection, projection.GetEvents().ToList());
                registeredProjections.Add(projectionWithEvents);
            }
        }

        public virtual ReplayDefinition UseProjection(IProjection projection)
        {
            var contractId = projection.GetType().GetContractId();

            // Ignore using projections which are not registered
            var projectionWithEvents = registeredProjections.FirstOrDefault(x => x.Projection.GetType().GetContractId() == contractId);
            if (ReferenceEquals(null, projectionWithEvents) == true)
                return this;

            // Ignore adding projections that are already added
            if (availableForReplayProjections.Any(x => x.Projection.GetType().GetContractId() == contractId) == true)
                return this;

            availableForReplayProjections.Add(projectionWithEvents);
            return this;
        }

        public virtual ReplayDefinition UseProjections(IEnumerable<IProjection> projections)
        {
            foreach (var projection in projections)
            {
                UseProjection(projection);
            }
            return this;
        }

        public abstract void OnNext(AggregateCommit aggregateCommit);

        public abstract void OnError(Exception error);

        public abstract void OnCompleted();

        public virtual IEnumerable<IProjectionWithEvents> RegisteredProjections
        {
            get { return registeredProjections.AsReadOnly(); }
        }

        public virtual IEnumerable<IProjectionWithEvents> AvailableForReplayProjections
        {
            get { return availableForReplayProjections.AsReadOnly(); }
        }
    }
}
