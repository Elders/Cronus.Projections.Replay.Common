﻿using System;
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

        public IReplayRulesManager Register(IEnumerable<IReplayRule> rules)
        {
            foreach (var rule in rules)
            {
                Register(rule);
            }
            return this;
        }

        public IReplayRulesManager Register(IReplayRule rule)
        {
            if (ReferenceEquals(null, rule) == true) throw new ArgumentNullException(nameof(rule));
            rules.Add(rule);
            return this;
        }

        public bool ShouldReplay(IProjectionWithEvents projectionWithEvents)
        {
            if (ReferenceEquals(null, projectionWithEvents) == true) throw new ArgumentNullException(nameof(projectionWithEvents));
            var result = rules.All(x => x.ShouldReplay(projectionWithEvents) == true);

            return result;
        }
    }
}
