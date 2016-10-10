using System;
using System.Collections.Generic;
using System.Linq;
using Elders.Cronus.DomainModeling;
using Elders.Cronus.Projections.Replay.Common.Extensions;

namespace Elders.Cronus.Projections.Replay.Common.Fixers
{
    public abstract class FixerAbastract
    {
        List<IProjectionWithEvents> InternalProjections;

        public FixerAbastract()
        {
            InternalProjections = new List<IProjectionWithEvents>();
        }

        public abstract void Replay(IEvent @event, long arCommitTimestamp);

        public virtual FixerAbastract AddProjection(IProjection projection, IEnumerable<Type> eventTypes = null)
        {
            var projectionWithEvents = new ProjectionWithEvents(projection, eventTypes ?? projection.GetEvents().ToList());
            InternalProjections.Add(projectionWithEvents);
            return this;
        }

        public virtual IEnumerable<IProjectionWithEvents> Projections
        {
            get { return InternalProjections.AsReadOnly(); }
        }
    }
}
