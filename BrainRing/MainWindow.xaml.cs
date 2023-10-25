using System.Windows;
using BrainRing.UI;

namespace BrainRing;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ModeInit();
    }

    //todo убрать это отсюда, сделать через DI или через ViewModel
    private void ModeInit()
    {
        MainGrid.Children.Clear();
        var props = Application.Current.Properties;
        if (props.Contains(App.MODE_NAME)
            && props[App.MODE_NAME] is App.EDIT_NAME)
        {
            MainGrid.Children.Add(new MainEditView());
        }
        else
        {
            MainGrid.Children.Add(new MainView());
        }
    }
}