using System.Threading.Tasks;
using System.Windows.Input;
using BrainRing.Core.Game;
using BrainRing.UI.Common;
using CommunityToolkit.Mvvm.Input;

namespace BrainRing.UI.Edit;

//todo: никак не отслежу ошибку: "DeferRefresh" не разрешено во время выполнения операции AddNew или EditItem
//System.Windows.Data.CollectionView.DeferRefresh()
//полезные ссылки:
//https://rsdn.org/forum/dotnet.gui/4001738.flat
//https://stackoverflow.com/questions/20204592/wpf-datagrid-refresh-is-not-allowed-during-an-addnew-or-edititem-transaction-m
//https://stackoverflow.com/questions/18281615/deferrefresh-is-not-allowed-during-an-addnew-or-edititem-transaction
public class ProcessEditViewModel : AbstractGoNextCommandContainer
{
    private object _currentContent;

    private RoundViewModel? _currentRound;
    private TopicViewModel? _currentTopic;
    private QuestionViewModel? _currentQuestion;

    private readonly string? _fileName;
    private bool? _notifyNavigating;

    public ProcessEditViewModel(string? filename, Pack? pack = null)
    {
        _fileName = filename;
        CurrentPack = new PackViewModel(pack);
        _currentContent = CurrentPack;
        GoToPackCommand = new AsyncRelayCommand(ExecuteGoToPack);
        GoToRoundCommand = new AsyncRelayCommand<RoundViewModel?>(ExecuteGoToRound);
        GoToTopicCommand = new AsyncRelayCommand<TopicViewModel?>(ExecuteGoToTopic);
        GoToQuestionCommand = new AsyncRelayCommand<QuestionViewModel?>(ExecuteGoToQuestion);
        SavePackCommand = new AsyncRelayCommand(ExecuteSavePack);
    }

    public ICommand GoToPackCommand { get; }
    public ICommand GoToRoundCommand { get; }
    public ICommand GoToTopicCommand { get; }
    public ICommand GoToQuestionCommand { get; }

    public ICommand SavePackCommand { get; }

    public object CurrentContent
    {
        get => _currentContent;
        set => SetAndRaise(ref _currentContent, value);
    }

    public PackViewModel CurrentPack { get; }

    public RoundViewModel? CurrentRound
    {
        get => _currentRound;
        set => SetAndRaise(ref _currentRound, value);
    }

    public TopicViewModel? CurrentTopic
    {
        get => _currentTopic;
        set => SetAndRaise(ref _currentTopic, value);
    }

    public QuestionViewModel? CurrentQuestion
    {
        get => _currentQuestion;
        set => SetAndRaise(ref _currentQuestion, value);
    }

    public bool? NotifyNavigating
    {
        get => _notifyNavigating;
        set => SetAndRaise(ref _notifyNavigating, value);
    }

    private async Task ExecuteGoToPack()
    {
        NotifyNavigating = true;
        CurrentQuestion = null;
        CurrentTopic = null;
        CurrentRound = null;
        CurrentContent = CurrentPack;
        await Task.Delay(250);
        NotifyNavigating = null;
    }

    private async Task ExecuteGoToRound(RoundViewModel? round)
    {
        NotifyNavigating = true;
        try
        {
            CurrentQuestion = null;
            CurrentTopic = null;

            if (round == null)
                CurrentContent = CurrentRound!;
            else
            {
                CurrentRound = round;
                CurrentContent = CurrentRound;
            }

            await Task.Delay(250);
        }
        finally
        {
            NotifyNavigating = null;
        }
    }

    private async Task ExecuteGoToTopic(TopicViewModel? topic)
    {
        NotifyNavigating = true;
        try
        {
            CurrentQuestion = null;

            if (topic == null)
                CurrentContent = CurrentTopic!;
            else
            {
                CurrentTopic = topic;
                CurrentContent = CurrentTopic;
            }
            NotifyNavigating = null;
            await Task.Delay(250);
        }
        finally
        {
            NotifyNavigating = null;
        }
    }

    private async Task ExecuteGoToQuestion(QuestionViewModel? question)
    {
        NotifyNavigating = true;
        try
        {
            if (question == null)
                return;

            CurrentQuestion = question;
            CurrentContent = CurrentQuestion;
            await Task.Delay(250);
        }
        finally
        {
            NotifyNavigating = null;
        }
    }

    private async Task ExecuteSavePack()
    {
        var pack = CurrentPack.GetPack();
        if (await ShowDialogService.SavePack(pack, _fileName))
            await ShowDialogService.ShowInfo("Файл успешно сохранён");
        else
            await ShowDialogService.ShowError("Ошибка при сохранении файла");
    }
}