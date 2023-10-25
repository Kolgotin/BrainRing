using BrainRing.Core.Game;
using BrainRing.Core.Interfaces;

namespace BrainRing.Core.Stages;

public class SetPlayersStage : IStage
{
    public SetPlayersStage(Pack pack)
    {
        Pack = pack;
    }

    public Pack Pack { get; }
    public List<Player>? Players { get; set; }

    public IStage Next()
    {
        if (Players is null || Players.Count == 0)
            throw new ArgumentException("Добавьте игроков!");

        return new GameStage(Pack, Players);
    }
}