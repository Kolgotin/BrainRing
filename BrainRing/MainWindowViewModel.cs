using BrainRing.UI;
using DynamicData.Binding;
using System.Windows;

namespace BrainRing;

public class MainWindowViewModel : AbstractNotifyPropertyChanged
{
    public MainWindowViewModel()
    {
        var props = Application.Current.Properties;
        if (props.Contains(App.MODE_NAME)
            && props[App.MODE_NAME] is App.EDIT_NAME)
        {
            MainContent = new MainEditViewModel();
        }
        else
        {
            MainContent = new MainViewModel();
        }
    }

    public object MainContent { get; }
}