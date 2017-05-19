using System;
using System.Collections.Generic;
using Elders.Cronus.DomainModeling;

namespace Elders.Cronus.Projections.Replay.Common.ReplayDefinition
{
    public class ProjectionWithEvents : IProjectionWithEvents
    {
        public ProjectionWithEvents(IProjection projection, IEnumerable<Type> events)
        {
            if (ReferenceEquals(null, projection) == true) throw new ArgumentNullException(nameof(projection));
            if (ReferenceEquals(null, events) == true) throw new ArgumentNullException(nameof(events));


            Projection = projection;
            Events = events;
        }

        public IProjection Projection { get; private set; }

        public IEnumerable<Type> Events { get; private set; }
    }
}
