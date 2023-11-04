using System;
using System.Threading.Tasks;
using BrainRing.Core.Interfaces;
using BrainRing.UI.Common;
using CommunityToolkit.Mvvm.Input;
using DynamicData.Binding;

namespace BrainRing.UI;

//todo вынести всё что можно в микросервисы
//todo добавить класс с DI
public class MainViewModel : AbstractNotifyPropertyChanged
{
    private IStageContainer _stageViewModel;

    public MainViewModel()
    {
        _stageViewModel = StageViewModelFactory.Create();
        StageViewModel.GoNextCommand = new AsyncRelayCommand(ChangeStage);
    }
    
    public IStageContainer StageViewModel
    {
        get => _stageViewModel;
        set => SetAndRaise(ref _stageViewModel, value);
    }
    
    private async Task ChangeStage()
    {
        try
        {
            var newStage = StageViewModel.NextStage();
            StageViewModel = StageViewModelFactory.Create(newStage);
            StageViewModel.GoNextCommand = new AsyncRelayCommand(ChangeStage);
        }
        catch (Exception e)
        {
            await ShowDialogService.ShowError(e.Message);
        }
    }
}