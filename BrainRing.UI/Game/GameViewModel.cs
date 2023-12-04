using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BrainRing.Core.Game;
using BrainRing.Core.Interfaces;
using BrainRing.Core.Stages;
using BrainRing.UI.Common;
using CommunityToolkit.Mvvm.Input;

namespace BrainRing.UI.Game;

//todo: проверить что диспозится всё что нужно
//todo: удалить асинхронщину везде где она не нужна
public sealed class GameViewModel : AbstractStageContainer, IDisposable
{
    private readonly GameStage _stage;
    private RoundViewModel? _currentRound; 
    private QuestionViewModel? _currentQuestion;
    private List<RoundBase>.Enumerator _enumerator;
    private object _currentContent;

    public GameViewModel(GameStage stage)
    {
        _stage = stage;
        Players = _stage.Players.Select(x => new PlayerViewModel(x)).ToList();
        SelectAnsweringViewModel = new SelectAnsweringViewModel(Players);
        _enumerator = _stage.Pack.Rounds.GetEnumerator();

        NextRoundCommand = new AsyncRelayCommand(ExecuteNextRound);
        ShowQuestionCommand = new AsyncRelayCommand<QuestionViewModel?>(ExecuteShowQuestion);
        AnswerCommand = new AsyncRelayCommand(ExecuteAnswer);
        SelectAnsweringCommand = new AsyncRelayCommand<PlayerViewModel?>(ExecuteSelectAnswering);
        ReturnToQuestionCommand = new AsyncRelayCommand(ExecuteReturnToQuestion);
        NextBlitzQuestionCommand = new AsyncRelayCommand(ExecuteNextBlitzQuestion);
        FinishRoundCommand = new AsyncRelayCommand(ExecuteFinishRound);
        NextRoundCommand.Execute(null);
    }

    public ICommand NextRoundCommand { get; }
    public ICommand ShowQuestionCommand { get; }
    public ICommand AnswerCommand { get; }
    public ICommand SelectAnsweringCommand { get; }
    public ICommand ReturnToQuestionCommand { get; }
    public ICommand NextBlitzQuestionCommand { get; }
    public ICommand FinishRoundCommand { get; }

    public List<PlayerViewModel> Players { get; }

    public SelectAnsweringViewModel SelectAnsweringViewModel { get; }

    public object CurrentContent
    {
        get => _currentContent;
        set => SetAndRaise(ref _currentContent, value);
    }

    public RoundViewModel? CurrentRound
    {
        get => _currentRound;
        set => SetAndRaise(ref _currentRound, value);
    }

    public QuestionViewModel? CurrentQuestion
    {
        get => _currentQuestion;
        set => SetAndRaise(ref _currentQuestion, value);
    }

    public override IStage NextStage()
    {
        return _stage.Next();
    }

    private async Task ExecuteNextRound()
    {
        if (CurrentRound is not null &&
            !await ShowDialogService.ShowYesNowMessage("Вы уверены что хотите завершить раунд?"))
            return;

        Players.ForEach(x => x.SaveScore());

        if (_enumerator.MoveNext())
        {
            CurrentRound?.Dispose(); //todo: а тут не упадёт?
            CurrentQuestion = null;
            CurrentRound = new RoundViewModel(_enumerator.Current);
            CurrentContent = CurrentRound;
        }
        else
        {
            GoNextCommand?.Execute(null);
        }
    }

    private Task ExecuteShowQuestion(QuestionViewModel? question)
    {
        if (question is null || question.IsAnswered)
            return Task.CompletedTask;

        CurrentQuestion = question;
        CurrentContent = question;
        return Task.CompletedTask;
    }

    private Task ExecuteAnswer()
    {
        CurrentContent = SelectAnsweringViewModel;
        return Task.CompletedTask;
    }

    private Task ExecuteSelectAnswering(PlayerViewModel? player)
    {
        CurrentQuestion?.Answer(player);
        CurrentQuestion = null;

        if (CurrentRound is null)
        {
            CurrentContent = new FinishRoundViewModel(Players);
            return Task.CompletedTask;
        }

        if (CurrentRound.Topics
            .Any(x => x.Questions.Any(y => !y.IsAnswered)))
            CurrentContent = CurrentRound;
        else
            CurrentContent = new FinishRoundViewModel(Players);

        return Task.CompletedTask;
    }

    private Task ExecuteReturnToQuestion()
    {
        if (CurrentQuestion is null)
            if (CurrentRound is null)
                CurrentContent = new FinishRoundViewModel(Players);
            else
                CurrentContent = CurrentRound;
        else
            CurrentContent = CurrentQuestion;
        return Task.CompletedTask;
    }

    //todo: протестить вариант если в блитц раунде нет вопросов
    private Task ExecuteNextBlitzQuestion()
    {
        if (CurrentRound is null)
        {
            CurrentContent = new FinishRoundViewModel(Players);
            return Task.CompletedTask;
        }

        CurrentRound.NextBlitzQuestionCommand.Execute(null);

        if (CurrentRound.CurrentQuestion is null)
            CurrentContent = new FinishRoundViewModel(Players);

        return Task.CompletedTask;
    }

    private async Task ExecuteFinishRound()
    {
        var confirmed =
            await ShowDialogService.ShowYesNowMessage("Вы уверены что хотите завершить раунд досрочно?");

        if (confirmed)
            CurrentContent = new FinishRoundViewModel(Players);
    }

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}