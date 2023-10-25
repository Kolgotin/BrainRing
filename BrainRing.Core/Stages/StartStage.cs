using BrainRing.Core.Game;
using BrainRing.Core.Interfaces;

namespace BrainRing.Core.Stages;

public class StartStage : IStage
{
    public Pack? Pack { get; set; }

    public IStage Next()
    {
        if (Pack is null)
            throw new ArgumentException("Не выбрана игра!");

        return new SetPlayersStage(Pack);
    }
}