using System.Collections.Generic;
using Elders.Cronus.Projections.Replay.Common.ReplayDefinition;

namespace Elders.Cronus.Projections.Replay.Common.ReplayRules
{
    public interface IReplayRulesManager
    {
        IReplayRulesManager Register(IReplayRule rule);

        IReplayRulesManager Register(IEnumerable<IReplayRule> rules);

        bool ShouldReplay(IProjectionWithEvents projectionWithEvents);
    }
}
