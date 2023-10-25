using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using BrainRing.Core.Game;
using BrainRing.Core.Interfaces;
using BrainRing.Core.Stages;
using BrainRing.Managers;
using BrainRing.UI.Common;
using CommunityToolkit.Mvvm.Input;
using DynamicData.Binding;
using Microsoft.Win32;

namespace BrainRing.UI.Edit;

public class StartEditViewModel : AbstractGoNextCommandContainer
{
    //todo добавить загрузку базы из excel
    //todo редактирование - лист где можно добавить раунд/тему/вопрос, провалиться внутрь
    //todo редактирование вопроса с выбором типа: обычный, фото, (в дальнейшем аудио)
    private Pack? _pack;
    private readonly MainEditViewModel _host;

    public StartEditViewModel(MainEditViewModel host)
    {
        _host = host;
        LoadExcelCommand = new AsyncRelayCommand(ExecuteLoadExcel);
        LoadJsonCommand = new AsyncRelayCommand(ExecuteLoadJson);
        GoNextCommand = new AsyncRelayCommand(ExecuteGoNext);
    }

    public ICommand LoadExcelCommand { get; }
    public ICommand LoadJsonCommand { get; }

    //todo эта хрень не работает
    private Task ExecuteLoadExcel()
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Excel files |*.xlsx;*.xls|All files (*.*)|*.*"
        };
        if (openFileDialog.ShowDialog() == true)
            FileManager.ReadExcelFile(openFileDialog.FileName);

        return Task.CompletedTask;
    }

    private Task ExecuteLoadJson()
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "json |*.json"
        };

        if (openFileDialog.ShowDialog() != true)
            return Task.CompletedTask;

        var pack = FileManager.ReadJsonFile(openFileDialog.FileName);
        _pack = pack ?? new Pack();

        return Task.CompletedTask;
    }

    private Task ExecuteGoNext()
    {
        _host.CurrentContent = new ProcessEditViewModel(_pack);
        return Task.CompletedTask;
    }

    private Pack DummyPack()
    {
        var pack = new Pack()
        {
            Name = "new pack",
            Rounds = new List<Round>()
            {
                new Round()
                {
                    Name = "round1",
                    Topics = new List<Topic>()
                    {
                        new Topic()
                        {
                            Name = "theme11",
                            Questions = new List<Question>()
                            {
                                new Question()
                                {
                                    Description = "Description111",
                                    Cost = 1,
                                },

                                new Question()
                                {
                                    Description = "Description112",
                                    Cost = 1,
                                },
                                new Question()
                                {
                                    Description = "Description113",
                                    Cost = 1,
                                }
                            }
                        },

                        new Topic()
                        {
                            Name = "theme12",
                            Questions = new List<Question>()
                            {
                                new Question()
                                {
                                    Description = "Description121",
                                    Cost = 1,
                                },

                                new Question()
                                {
                                    Description = "Description122",
                                    Cost = 1,
                                },
                                new Question()
                                {
                                    Description = "Description123",
                                    Cost = 1,
                                }
                            }
                        }
                    }
                }
            }
        };
        return pack;
    }
}