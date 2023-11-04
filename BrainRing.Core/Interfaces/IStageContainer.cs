namespace BrainRing.Core.Interfaces;

public interface IStageContainer : IGoNextCommandContainer
{
    IStage NextStage();
}