using System;
using BrainRing.UI;
using DynamicData.Binding;
using System.Windows;
using NLog;

namespace BrainRing;

public class MainWindowViewModel : AbstractNotifyPropertyChanged
{
    private readonly Logger _logger;

    public MainWindowViewModel()
    {
        AppDomain currentDomain = AppDomain.CurrentDomain;
        currentDomain.UnhandledException += MyHandler;

        _logger = LogManager.GetCurrentClassLogger();
        _logger.Info("Приложение стартовало");
        var props = Application.Current.Properties;

        if (props.Contains(App.MODE_NAME) && props[App.MODE_NAME] is App.EDIT_NAME)
        {
            MainContent = new MainEditViewModel();
        }
        else
        {
            MainContent = new MainViewModel();
        }
    }

    public object MainContent { get; }

    private void MyHandler(object sender, UnhandledExceptionEventArgs args)
    {
        _logger.Error(args.ExceptionObject);
    }
}