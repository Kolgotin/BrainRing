using BrainRing.UI;
using DynamicData.Binding;
using System.Windows;

namespace BrainRing;

//Создать ярлык с относительным путём
//https://www.cyberforum.ru/windows10/thread2693116.html

//Как создать и запустить bat-файлы
//https://www.nic.ru/help/kak-sozdat6-i-zapustit6-bat-fajly_11640.html
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