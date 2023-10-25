using BrainRing.UI.Common;
using BrainRing.UI.Edit;
using DynamicData.Binding;

namespace BrainRing.UI;

public class MainEditViewModel : AbstractNotifyPropertyChanged
{
    private AbstractGoNextCommandContainer _currentContent;

    public MainEditViewModel()
    {
        _currentContent = new StartEditViewModel(this);
    }

    public AbstractGoNextCommandContainer CurrentContent
    {
        get => _currentContent;
        set => SetAndRaise(ref _currentContent, value);
    }
}