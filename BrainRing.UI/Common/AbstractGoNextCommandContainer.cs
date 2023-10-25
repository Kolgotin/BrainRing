using System.Windows.Input;
using BrainRing.Core.Interfaces;
using DynamicData.Binding;

namespace BrainRing.UI.Common;

public abstract class AbstractGoNextCommandContainer : AbstractNotifyPropertyChanged, IGoNextCommandContainer
{
    private ICommand? _nextStageCommand;

    public ICommand? GoNextCommand
    {
        get => _nextStageCommand;
        set => SetAndRaise(ref _nextStageCommand, value);
    }
}