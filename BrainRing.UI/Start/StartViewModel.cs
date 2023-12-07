using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BrainRing.Core.Game;
using BrainRing.Core.Interfaces;
using BrainRing.Core.Stages;
using BrainRing.Managers;
using BrainRing.UI.Common;
using CommunityToolkit.Mvvm.Input;

namespace BrainRing.UI.Start;

//todo: добавить крутилку пока грузится пак
public class StartViewModel : AbstractStageContainer
{
    private readonly StartStage _stage;

    public StartViewModel() : this(new StartStage())
    {
    }

    public StartViewModel(StartStage startStage)
    {
        _stage = startStage;
        LoadPackCommand = new AsyncRelayCommand(ExecuteLoadPack);
    }

    public ICommand LoadPackCommand { get; }

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

    private async Task ExecuteLoadPack()
    {
        try
        {
            var fileName = await ShowDialogService.SelectPackFilePath();

            if (fileName is null)
                return;

            var pack = await FileManager.ReadPackFile(fileName);

            if (pack is null || pack.Rounds.Count == 0)
            {
                await ShowDialogService.ShowInfo("Не удалось загрузить список вопросов");
            }
            else
            {
                Pack = pack;
            }
        }
        catch (Exception e)
        {
            await ShowDialogService.ShowError(e.Message);
        }
    }
}