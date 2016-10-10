using System;
using System.Collections.Generic;
using Elders.Cronus.DomainModeling;

namespace Elders.Cronus.Projections.Replay.Common.Fixers
{
    public class ProjectionWithEvents : IProjectionWithEvents
    {
        public ProjectionWithEvents(IProjection projection, IEnumerable<Type> events)
        {
            Projection = projection;
            Events = events;
        }

        public IProjection Projection { get; private set; }

        public IEnumerable<Type> Events { get; private set; }
    }
}
