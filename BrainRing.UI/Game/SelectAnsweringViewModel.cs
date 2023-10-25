using BrainRing.UI.Common;
using System.Collections.Generic;

namespace BrainRing.UI.Game;

public class SelectAnsweringViewModel
{
    public SelectAnsweringViewModel(List<PlayerViewModel> players)
    {
        Players = players;
    }

    public List<PlayerViewModel> Players { get; }
}