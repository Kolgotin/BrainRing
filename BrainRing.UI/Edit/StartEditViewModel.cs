using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using BrainRing.Core.Game;
using BrainRing.Managers;
using BrainRing.UI.Common;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace BrainRing.UI.Edit;

//todo: добавить загрузку вопросов из excel
public class StartEditViewModel : AbstractGoNextCommandContainer
{
    private Pack? _pack;
    private readonly MainEditViewModel _host;
    private string _fileName;

    public StartEditViewModel(MainEditViewModel host)
    {
        _host = host;
        LoadJsonCommand = new AsyncRelayCommand(ExecuteLoadJson);
        GoNextCommand = new AsyncRelayCommand(ExecuteGoNext);
    }

    public ICommand LoadJsonCommand { get; }

    public string FileName
    {
        get => _fileName;
        set => SetAndRaise(ref _fileName, value);
    }

    private async Task ExecuteLoadJson()
    {
        try
        {
            var fullFileName = await ShowDialogService.SelectJsonFilePath();

            if (fullFileName is null)
                return;

            var pack = FileManager.ReadJsonFile(fullFileName);
            FileName = Path.GetFileName(fullFileName);
            _pack = pack ?? new Pack();
        }
        catch (Exception e)
        {
            await ShowDialogService.ShowError(e.Message);
        }
    }

    private Task ExecuteGoNext()
    {
        _host.CurrentContent = new ProcessEditViewModel(FileName, _pack);
        return Task.CompletedTask;
    }
}