using System.Collections.ObjectModel;
using System.Linq;
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
        AddRoundCommand = new RelayCommand(ExecuteAddRound);
        RemoveRoundCommand = new RelayCommand<RoundViewModel>(ExecuteRemoveRound);
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

    private void ExecuteAddRound()
    {
        Rounds.Add(new RoundViewModel());
    }

    private void ExecuteRemoveRound(RoundViewModel? round)
    {
        if (round is not null && Rounds.Contains(round))
            Rounds.Remove(round);
    }
}