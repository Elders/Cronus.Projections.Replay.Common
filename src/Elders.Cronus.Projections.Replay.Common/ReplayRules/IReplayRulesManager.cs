using Elders.Cronus.Projections.Replay.Common.ReplayDefinition;

namespace Elders.Cronus.Projections.Replay.Common.ReplayRules
{
    public interface IReplayRulesManager
    {
        IReplayRulesManager Register(IReplayRule rule);

        bool ShouldReplay(IProjectionWithEvents projectionWithEvents);
    }
}
