using System.Collections.Generic;
using System.Linq;
using BrainRing.UI.Common;

namespace BrainRing.UI.Game;

public class FinishRoundViewModel
{
    public FinishRoundViewModel(List<PlayerViewModel> players)
    {
        Players = players.OrderByDescending(x => x.Score).ToList();
    }

    public List<PlayerViewModel> Players { get; }
}