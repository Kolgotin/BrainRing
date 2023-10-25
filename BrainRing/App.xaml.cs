using System.Linq;
using System.Windows;

namespace BrainRing;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public const string MODE_NAME = "mode";
    public const string EDIT_NAME = "edit";

    private void App_OnStartup(object sender, StartupEventArgs e)
    {
        if (e.Args.Contains(EDIT_NAME))
            Properties.Add(MODE_NAME, EDIT_NAME);
    }
}