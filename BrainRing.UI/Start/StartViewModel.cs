using System.Threading.Tasks;
using System.Windows.Input;
using BrainRing.Core.Game;
using BrainRing.Core.Interfaces;
using BrainRing.Core.Stages;
using BrainRing.Managers;
using BrainRing.UI.Common;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace BrainRing.UI.Start;

public class StartViewModel : AbstractStageContainer
{
    private readonly StartStage _stage;

    public StartViewModel() : this(new StartStage())
    {
    }

    public StartViewModel(StartStage startStage)
    {
        _stage = startStage;
        LoadJsonCommand = new AsyncRelayCommand(ExecuteLoadJson);
    }

    public ICommand LoadJsonCommand { get; }

    public Pack? Pack
    {
        get => _stage.Pack;
        set
        {
            if (Equals(value, _stage.Pack)) return;
            _stage.Pack = value;
            OnPropertyChanged();
        }
    }

    public override IStage NextStage()
    {
        return _stage.Next();
    }

    private Task ExecuteLoadJson()
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "json |*.json"
        };

        if (openFileDialog.ShowDialog() != true)
            return Task.CompletedTask;

        var pack = FileManager.ReadJsonFile(openFileDialog.FileName);

        if (pack is null)
        {
            //show message
        }
        else
        {
            Pack = pack;
        }

        return Task.CompletedTask;
    }
    
}