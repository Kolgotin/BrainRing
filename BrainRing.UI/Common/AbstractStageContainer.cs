using BrainRing.Core.Interfaces;

namespace BrainRing.UI.Common;

public abstract class AbstractStageContainer : AbstractGoNextCommandContainer, IStageContainer
{
    public abstract IStage NextStage();
}