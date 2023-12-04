using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using BrainRing.Core.Game;
using BrainRing.Managers;
using BrainRing.UI.Common;
using CommunityToolkit.Mvvm.Input;

namespace BrainRing.UI.Edit;

//todo: добавить загрузку вопросов из excel
public class StartEditViewModel : AbstractGoNextCommandContainer
{
    private Pack? _pack;
    private readonly MainEditViewModel _host;
    private string? _fileName;

    public StartEditViewModel(MainEditViewModel host)
    {
        _host = host;
        LoadPackCommand = new AsyncRelayCommand(ExecuteLoadPack);
        GoNextCommand = new AsyncRelayCommand(ExecuteGoNext);
    }

    public ICommand LoadPackCommand { get; }

    public string? FileName
    {
        get => _fileName;
        set => SetAndRaise(ref _fileName, value);
    }

    private async Task ExecuteLoadPack()
    {
        try
        {
            var fullFileName = await ShowDialogService.SelectPackFilePath();

            if (fullFileName is null)
                return;

            var pack = FileManager.ReadPackFile(fullFileName);
            FileName = Path.GetFileName(fullFileName);
            _pack = pack ?? new Pack();
            await ExecuteGoNext();
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