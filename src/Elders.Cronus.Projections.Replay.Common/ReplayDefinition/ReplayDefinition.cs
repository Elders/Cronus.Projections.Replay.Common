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
            if (registeredProjections.Any(x => x.Projection.GetType().GetContractId() == projection.GetType().GetContractId()) == false)
                throw new Exception($"Projection {projection.GetType().Name} cannot be used because it is not registered in the replay definition");

            var projectionWithEvents = new ProjectionWithEvents(projection, projection.GetEvents().ToList());
            availableForReplayProjections.Add(projectionWithEvents);
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
