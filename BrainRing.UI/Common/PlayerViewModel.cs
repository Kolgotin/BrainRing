using System.Numerics;
using System.Threading.Tasks;
using BrainRing.Core;
using DynamicData.Binding;

namespace BrainRing.UI.Common;

public class PlayerViewModel : AbstractNotifyPropertyChanged
{
    public PlayerViewModel()
    {
        Player = new Player();
    }

    public PlayerViewModel(Player player)
    {
        Player = player;
    }
    
    public Player Player { get; }

    public string Name
    {
        get => Player.Name;
        set
        {
            if (value == Player.Name) return;
            Player.Name = value;
            OnPropertyChanged();
        }
    }

    public int Score
    {
        get => Player.Score;
        set
        {
            if (value == Player.Score) return;
            Player.Score = value;
            OnPropertyChanged();
        }
    }
}