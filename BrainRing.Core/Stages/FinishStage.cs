using BrainRing.Core.Interfaces;

namespace BrainRing.Core.Stages;

public class FinishStage : IStage
{
    public FinishStage(IReadOnlyList<Player> players)
    {
        Players = players;
    }
    
    public IReadOnlyList<Player> Players { get; }

    public IStage Next()
    {
        return new StartStage();
    }
}