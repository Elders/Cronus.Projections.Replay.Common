using System;
using System.Collections.Generic;
using System.Linq;
using Elders.Cronus.DomainModeling;
using Elders.Cronus.EventStore;
using Elders.Cronus.Projections.Replay.Common.Extensions;
using Elders.Cronus.Projections.Replay.Common.ReplayRules;

namespace Elders.Cronus.Projections.Replay.Common.ReplayDefinition
{
    public abstract class ReplayDefinition
    {
        List<IProjectionWithEvents> internalProjections;

        readonly IReplayRulesManager defaultReplayRuleManager;

        public ReplayDefinition()
        {
            internalProjections = new List<IProjectionWithEvents>();
            defaultReplayRuleManager = new DefaultReplayRulesManager();
        }

        public abstract void Replay(AggregateCommit aggregateCommit);

        public virtual ReplayDefinition AddProjection(IProjection projection, IEnumerable<Type> eventTypes = null)
        {
            var projectionWithEvents = new ProjectionWithEvents(projection, eventTypes ?? projection.GetEvents().ToList());
            internalProjections.Add(projectionWithEvents);
            return this;
        }

        public virtual ReplayDefinition AddProjections(IEnumerable<IProjection> projections)
        {
            foreach (var projection in projections)
            {
                var projectionWithEvents = new ProjectionWithEvents(projection, projection.GetEvents().ToList());
                internalProjections.Add(projectionWithEvents);
            }

            return this;
        }

        public virtual IEnumerable<IProjectionWithEvents> Projections
        {
            get { return internalProjections.AsReadOnly(); }
        }

        public virtual IReplayRulesManager ReplayRuleManager { get { return defaultReplayRuleManager; } }
    }
}
