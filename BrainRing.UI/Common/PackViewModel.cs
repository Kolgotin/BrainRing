using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BrainRing.Core.Game;
using CommunityToolkit.Mvvm.Input;
using DynamicData.Binding;

namespace BrainRing.UI.Common;

public class PackViewModel : AbstractNotifyPropertyChanged
{
    private string _name;
    
    public PackViewModel(Pack? pack = null)
    {
        pack ??= new Pack();
        _name = pack.Name;
        Rounds = new ObservableCollection<RoundViewModel>(
            pack.Rounds
                .Select(x => new RoundViewModel(x))
                .ToList());
        AddRoundCommand = new AsyncRelayCommand(ExecuteAddRound);
        RemoveRoundCommand = new AsyncRelayCommand<RoundViewModel>(ExecuteRemoveRound);
    }

    public ICommand AddRoundCommand { get; }
    public ICommand RemoveRoundCommand { get; }

    public string Name
    {
        get => _name;
        set => SetAndRaise(ref _name, value);
    }

    public ObservableCollection<RoundViewModel> Rounds { get; }

    public Pack GetPack() =>
        new()
        {
            Name = Name,
            Rounds = Rounds.Select(x => x.GetRound()).ToList()
        };

    private Task ExecuteAddRound()
    {
        Rounds.Add(new RoundViewModel());
        return Task.CompletedTask;
    }

    private Task ExecuteRemoveRound(RoundViewModel? round)
    {
        if (round is not null && Rounds.Contains(round))
            Rounds.Remove(round);
        return Task.CompletedTask;
    }
}