using BrainRing.Core.Interfaces;
using BrainRing.Core.Stages;
using BrainRing.UI.Finish;
using BrainRing.UI.Game;
using BrainRing.UI.SetPlayers;
using BrainRing.UI.Start;

namespace BrainRing.UI.Common;

public class StageViewModelFactory
{
    public static IStageContainer Create(IStage? stage = null)
    {
        return stage switch
        {
            StartStage startStage => new StartViewModel(startStage),
            SetPlayersStage startStage => new SetPlayersViewModel(startStage),
            GameStage gameStage => new GameViewModel(gameStage),
            FinishStage finishStage => new FinishViewModel(finishStage),
            _ => new StartViewModel(),
        };
    }
}