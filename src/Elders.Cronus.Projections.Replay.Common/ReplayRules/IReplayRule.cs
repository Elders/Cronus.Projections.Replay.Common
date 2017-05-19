using Elders.Cronus.Projections.Replay.Common.ReplayDefinition;

namespace Elders.Cronus.Projections.Replay.Common.ReplayRules
{
    public interface IReplayRule
    {
        bool ShouldReplay(IProjectionWithEvents projectionWithEvents);
    }
}
