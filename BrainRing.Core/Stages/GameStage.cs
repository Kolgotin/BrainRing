using BrainRing.Core.Game;
using BrainRing.Core.Interfaces;

namespace BrainRing.Core.Stages;

public class GameStage : IStage
{
    public GameStage(Pack pack, IReadOnlyList<Player> players)
    {
        Pack = pack;
        Players = players;
    }

    public Pack Pack { get; }
 
    public IReadOnlyList<Player> Players { get; }

    public IStage Next()
    {
        return new FinishStage(Players);
    }
}