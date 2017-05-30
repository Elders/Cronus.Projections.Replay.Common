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
        List<IProjectionWithEvents> internalProjections;

        public ReplayDefinition()
        {
            internalProjections = new List<IProjectionWithEvents>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projection">The projection for replay</param>
        /// <param name="eventTypes">Specific event types that should be handled (handles all by default)</param>
        /// <returns></returns>
        public virtual ReplayDefinition AddProjection(IProjection projection, IEnumerable<Type> eventTypes = null)
        {
            var projectionWithEvents = new ProjectionWithEvents(projection, eventTypes ?? projection.GetEvents().ToList());
            internalProjections.Add(projectionWithEvents);
            return this;
        }

        public virtual ReplayDefinition AddProjections(IEnumerable<IProjection> projections)
        {
            foreach (var projection in projections)
            {
                var projectionWithEvents = new ProjectionWithEvents(projection, projection.GetEvents().ToList());
                internalProjections.Add(projectionWithEvents);
            }

            return this;
        }

        public abstract void OnNext(AggregateCommit aggregateCommit);

        public abstract void OnError(Exception error);

        public abstract void OnCompleted();

        public virtual IEnumerable<IProjectionWithEvents> Projections
        {
            get { return internalProjections.AsReadOnly(); }
        }
    }
}
