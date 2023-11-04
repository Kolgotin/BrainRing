using System.Threading.Tasks;
using System.Windows.Input;
using BrainRing.Core.Game;
using BrainRing.UI.Common;
using CommunityToolkit.Mvvm.Input;

namespace BrainRing.UI.Edit;

public class ProcessEditViewModel : AbstractGoNextCommandContainer
{
    private object _currentContent;

    private readonly PackViewModel _currentPack;
    private RoundViewModel? _currentRound;
    private TopicViewModel? _currentTopic;
    private QuestionViewModel? _currentQuestion;

    private readonly string? _fileName;

    public ProcessEditViewModel(string? filename, Pack? pack = null)
    {
        _fileName = filename;
        _currentPack = new PackViewModel(pack);
        _currentContent = _currentPack;
        GoToPackCommand = new AsyncRelayCommand(ExecuteGoToPack);
        GoToRoundCommand = new AsyncRelayCommand<RoundViewModel?>(ExecuteGoToRound);
        GoToTopicCommand = new AsyncRelayCommand<TopicViewModel?>(ExecuteGoToTopic);
        GoToQuestionCommand = new AsyncRelayCommand<QuestionViewModel?>(ExecuteGoToQuestion);
        SaveJsonCommand = new AsyncRelayCommand(ExecuteSaveJson);
    }

    public ICommand GoToPackCommand { get; }
    public ICommand GoToRoundCommand { get; }
    public ICommand GoToTopicCommand { get; }
    public ICommand GoToQuestionCommand { get; }

    public ICommand SaveJsonCommand { get; }

    public object CurrentContent
    {
        get => _currentContent;
        set => SetAndRaise(ref _currentContent, value);
    }

    private Task ExecuteGoToPack()
    {
        _currentQuestion = null;
        _currentTopic = null;
        _currentRound = null;
        CurrentContent = _currentPack;
        return Task.CompletedTask;
    }

    private Task ExecuteGoToRound(RoundViewModel? round)
    {
        _currentQuestion = null;
        _currentTopic = null;

        if (round == null)
            CurrentContent = _currentRound!;
        else
        {
            _currentRound = round;
            CurrentContent = _currentRound;
        }
        return Task.CompletedTask;
    }

    private Task ExecuteGoToTopic(TopicViewModel? topic)
    {
        _currentQuestion = null;

        if (topic == null)
            CurrentContent = _currentTopic!;
        else
        {
            _currentTopic = topic;
            CurrentContent = _currentTopic;
        }
        return Task.CompletedTask;
    }

    private Task ExecuteGoToQuestion(QuestionViewModel? question)
    {
        if (question == null)
            return Task.CompletedTask;

        _currentQuestion = question;
        CurrentContent = _currentQuestion;
        return Task.CompletedTask;
    }

    private async Task ExecuteSaveJson()
    {
        var pack = _currentPack.GetPack();
        if (await ShowDialogService.SavePack(pack, _fileName))
            await ShowDialogService.ShowInfo("Файл успешно сохранён");
        else
            await ShowDialogService.ShowError("Ошибка при сохранении файла");
    }
}