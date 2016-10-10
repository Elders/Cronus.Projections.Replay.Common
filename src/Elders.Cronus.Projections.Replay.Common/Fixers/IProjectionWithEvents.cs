using System;
using System.Collections.Generic;
using Elders.Cronus.DomainModeling;

namespace Elders.Cronus.Projections.Replay.Common.Fixers
{
    public interface IProjectionWithEvents
    {
        IProjection Projection { get; }
        IEnumerable<Type> Events { get; }
    }
}
