using System.Collections.Generic;
using System.Linq;
using BrainRing.Core.Interfaces;
using BrainRing.Core.Stages;
using BrainRing.UI.Common;

namespace BrainRing.UI.Finish;

public class FinishViewModel : AbstractStageContainer
{
    public FinishViewModel(FinishStage finishStage)
    {
        Players = finishStage.Players
            .Select(x=> new PlayerViewModel(x))
            .OrderByDescending(x => x.Score)
            .ToList();
    }

    public List<PlayerViewModel> Players { get; }

    public override IStage NextStage()
    {
        throw new System.NotImplementedException();
    }
}