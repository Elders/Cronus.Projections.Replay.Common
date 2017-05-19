using System;
using System.Collections.Generic;
using System.Linq;
using Elders.Cronus.DomainModeling;
using Elders.Cronus.EventStore;
using Elders.Cronus.Projections.Replay.Common.Extensions;

namespace Elders.Cronus.Projections.Replay.Common.ReplayDefinition
{
    public abstract class ReplayDefinition
    {
        List<IProjectionWithEvents> InternalProjections;

        public ReplayDefinition()
        {
            InternalProjections = new List<IProjectionWithEvents>();
        }

        public abstract void Replay(AggregateCommit aggregateCommit);

        public virtual ReplayDefinition AddProjection(IProjection projection, IEnumerable<Type> eventTypes = null)
        {
            var projectionWithEvents = new ProjectionWithEvents(projection, eventTypes ?? projection.GetEvents().ToList());
            InternalProjections.Add(projectionWithEvents);
            return this;
        }

        public virtual ReplayDefinition AddProjections(IEnumerable<IProjection> projections)
        {
            foreach (var projection in projections)
            {
                var projectionWithEvents = new ProjectionWithEvents(projection, projection.GetEvents().ToList());
                InternalProjections.Add(projectionWithEvents);
            }

            return this;
        }

        public virtual IEnumerable<IProjectionWithEvents> Projections
        {
            get { return InternalProjections.AsReadOnly(); }
        }
    }
}
