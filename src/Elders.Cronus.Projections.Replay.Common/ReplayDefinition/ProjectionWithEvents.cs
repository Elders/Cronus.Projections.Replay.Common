using System;
using System.Collections.Generic;
using Elders.Cronus.DomainModeling;
using Elders.Cronus.Projections.Replay.Common.ReplayRules;

namespace Elders.Cronus.Projections.Replay.Common.ReplayDefinition
{
    public class ProjectionWithEvents : IProjectionWithEvents
    {
        readonly IReplayRulesManager replayRulesManager;

        public ProjectionWithEvents(IProjection projection, IEnumerable<Type> events, IReplayRulesManager replayRulesManager)
        {

            if (ReferenceEquals(null, projection) == true) throw new ArgumentNullException(nameof(projection));
            if (ReferenceEquals(null, events) == true) throw new ArgumentNullException(nameof(events));
            if (ReferenceEquals(null, replayRulesManager) == true) throw new ArgumentNullException(nameof(replayRulesManager));

            Projection = projection;
            Events = events;
            this.replayRulesManager = replayRulesManager;

        }

        public ProjectionWithEvents(IProjection projection, IEnumerable<Type> events)
            : this(projection, events, new DefaultReplayRulesManager())
        { }

        public IProjection Projection { get; private set; }

        public IEnumerable<Type> Events { get; private set; }

        public IReplayRulesManager ReplayRuleManager
        {
            get
            {
                return replayRulesManager;
            }
        }
    }
}
