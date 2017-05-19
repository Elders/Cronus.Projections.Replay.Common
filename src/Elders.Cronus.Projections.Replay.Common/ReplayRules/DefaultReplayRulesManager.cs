using System.Collections.Generic;
using System.Linq;
using Elders.Cronus.Projections.Replay.Common.ReplayDefinition;

namespace Elders.Cronus.Projections.Replay.Common.ReplayRules
{
    public class DefaultReplayRulesManager : IReplayRulesManager
    {
        IList<IReplayRule> rules;

        public DefaultReplayRulesManager()
        {
            rules = new List<IReplayRule>();
        }

        public IReplayRulesManager Register(IReplayRule rule)
        {
            rules.Add(rule);
            return this;
        }

        public bool ShouldReplay(IProjectionWithEvents projectionWithEvents)
        {
            var result = rules.All(x => x.ShouldReplay(projectionWithEvents) == true);

            return result;
        }
    }
}
