using System;
using System.Collections.Generic;
using Elders.Cronus.DomainModeling;
using Elders.Cronus.Projections.Replay.Common.ReplayRules;

namespace Elders.Cronus.Projections.Replay.Common.ReplayDefinition
{
    public interface IProjectionWithEvents
    {
        IProjection Projection { get; }

        IEnumerable<Type> Events { get; }

        IReplayRulesManager ReplayRuleManager { get; }
    }
}
