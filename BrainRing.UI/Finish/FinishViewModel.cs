using System.Collections.Generic;
using System.Linq;
using BrainRing.Core.Interfaces;
using BrainRing.Core.Stages;
using BrainRing.UI.Common;

namespace BrainRing.UI.Finish;

public class FinishViewModel : AbstractStageContainer
{
    private FinishStage _finishStage;

    public FinishViewModel(FinishStage finishStage)
    {
        _finishStage = finishStage;
        Players = finishStage.Players
            .Select(x=> new PlayerViewModel(x))
            .OrderByDescending(x => x.Score)
            .ToList();
    }

    public List<PlayerViewModel> Players { get; }

    public override IStage NextStage()
    {
        return _finishStage.Next();
    }
}