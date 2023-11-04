using System;
using System.Threading.Tasks;
using System.Windows;
using BrainRing.Core.Game;
using BrainRing.Managers;
using Microsoft.Win32;

namespace BrainRing.UI.Common;

public class ShowDialogService
{
    public static Task<bool> ShowYesNowMessage(string message, string caption = "Подтверждение")
    {
        MessageBoxButton button = MessageBoxButton.YesNo;
        MessageBoxImage icon = MessageBoxImage.Question;
        var result = MessageBox.Show(message, caption, button, icon);
        return Task.FromResult(result == MessageBoxResult.Yes);
    }

    public static Task ShowInfo(string message, string caption = "Внимание")
    {
        _ = MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        return Task.CompletedTask;
    }

    public static Task ShowError(string message, string caption = "Ошибка")
    {
        _ = MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        return Task.CompletedTask;
    }
    
    public static async Task<string?> SelectJsonFilePath()
    {
        try
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "json |*.json"
            };

            var result = openFileDialog.ShowDialog() != true ? null : openFileDialog.FileName;
            return result;
        }
        catch (Exception e)
        {
            await ShowError(e.Message);
            return default;
        }
    }

    public static Task<bool> SavePack(Pack pack, string? filename)
    {
        try
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "json |*.json",
                FileName = filename ?? "default"
            };
            if (saveFileDialog.ShowDialog() == true)
                FileManager.WriteFileManager(pack, saveFileDialog.FileName);
        }
        catch (Exception e)
        {
            //todo: логгер чтоли добавить
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }
}