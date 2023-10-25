using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BrainRing.Core.Interfaces;
using BrainRing.Core.Stages;
using BrainRing.UI.Common;
using CommunityToolkit.Mvvm.Input;

namespace BrainRing.UI.SetPlayers;

public class SetPlayersViewModel : AbstractStageContainer
{
    private readonly SetPlayersStage _stage;

    private ObservableCollection<PlayerViewModel> _players;

    public SetPlayersViewModel(SetPlayersStage setPlayersStage)
    {
        _stage = setPlayersStage;
        _players = new ObservableCollection<PlayerViewModel>();
        AddPlayerCommand = new AsyncRelayCommand(ExecuteAddPlayer);
        RemovePlayerCommand = new AsyncRelayCommand<PlayerViewModel>(ExecuteRemovePlayer);
    }
    
    public ICommand AddPlayerCommand { get; }
    public ICommand RemovePlayerCommand { get; }

    public ObservableCollection<PlayerViewModel> Players
    {
        get => _players;
        set => SetAndRaise(ref _players, value);
    }

    public override IStage NextStage()
    {
        var players = Players
            .Where(x => !string.IsNullOrWhiteSpace(x.Name))
            .Select(x => x.Player)
            .ToList();
        _stage.Players = players;
        return _stage.Next();
    }

    private Task ExecuteAddPlayer()
    {
        Players.Add(new PlayerViewModel());
        return Task.CompletedTask;
    }

    private Task ExecuteRemovePlayer(PlayerViewModel? player)
    {
        if (player is null)
            return Task.CompletedTask;

        Players.Remove(player);
        return Task.CompletedTask;
    }
}