using Elders.Cronus.Projections.Replay.Common.ReplayDefinition;

namespace Elders.Cronus.Projections.Replay.Common.ReplayRules
{
    public class PositiveReplayRule : IReplayRule
    {
        bool IReplayRule.ShouldReplay(IProjectionWithEvents projectionWithEvents)
        {
            return true;
        }
    }
}
