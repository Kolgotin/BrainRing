using System.Reactive.Linq;
using BrainRing.Core;
using DynamicData.Binding;

namespace BrainRing.UI.Common;

public class PlayerViewModel : AbstractNotifyPropertyChanged
{
    private int _roundScore;

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

    public int Score => _roundScore + Player.Score;

    public int RoundScore
    {
        get => _roundScore;
        set
        {
            SetAndRaise(ref _roundScore, value);
            OnPropertyChanged(nameof(Score));
        }
    }

    public void SaveScore()
    {
        Player.Score = _roundScore + Player.Score;
        _roundScore = 0;
    }
}