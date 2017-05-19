using System.Collections.Generic;
using Elders.Cronus.Projections.Replay.Common.ReplayDefinition;

namespace Elders.Cronus.Projections.Replay.Common.ReplayRules
{
    public interface IReplayRulesManager
    {
        void Register(IReplayRule rule);

        void Register(IEnumerable<IReplayRule> rules);

        bool ShouldReplay(IProjectionWithEvents projectionWithEvents);
    }
}
