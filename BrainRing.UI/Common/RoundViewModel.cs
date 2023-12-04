using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BrainRing.Core.Game;
using CommunityToolkit.Mvvm.Input;
using DynamicData.Binding;

namespace BrainRing.UI.Common;

public class RoundViewModel : AbstractNotifyPropertyChanged,IDisposable
{
    private RoundTypes _selectedQuestionType;
    private string _name;
    private QuestionViewModel? _currentQuestion;
    private readonly IEnumerator<QuestionViewModel>? _enumerator;

    public RoundViewModel(RoundBase? round = null)
    {
        round ??= new SimpleRound();
        _name = round.Name;

        List<TopicViewModel> list;

        switch (round)
        {
            case BlitzRound b:
                SelectedRoundType = RoundTypes.BlitzRound;
                var topic = new TopicViewModel(b.Topic);
                list = new List<TopicViewModel>()
                {
                    topic
                };
                _enumerator = topic.Questions.GetEnumerator();
                break;
            case SimpleRound _:
            default:
                SelectedRoundType = RoundTypes.SimpleRound;
                list = round.GetTopics()
                    .Select(x => new TopicViewModel(x))
                    .ToList();
                break;
        }

        Topics = new ObservableCollection<TopicViewModel>(list);

        AddTopicCommand = new AsyncRelayCommand(ExecuteAddTopic);
        RemoveQuestionCommand = new AsyncRelayCommand<TopicViewModel>(ExecuteRemoveTopic);
        NextBlitzQuestionCommand = new AsyncRelayCommand(ExecuteNextBlitzQuestion);
    }

    public ICommand AddTopicCommand { get; }
    public ICommand RemoveQuestionCommand { get; }
    public ICommand NextBlitzQuestionCommand { get; }

    public RoundTypes SelectedRoundType
    {
        get => _selectedQuestionType;
        set => SetAndRaise(ref _selectedQuestionType, value);
    }

    public string Name
    {
        get => _name;
        set => SetAndRaise(ref _name, value);
    }

    public ObservableCollection<TopicViewModel> Topics { get; }

    public QuestionViewModel? CurrentQuestion
    {
        get => _currentQuestion;
        set => SetAndRaise(ref _currentQuestion, value);
    }

    public RoundBase GetRound()
    {
        return SelectedRoundType switch
        {
            RoundTypes.BlitzRound => new BlitzRound
            {
                Name = Name,
                Topic = new Topic
                {
                    Name = Name,
                    Questions = Topics.SelectMany(x => x.Questions)
                        .Select(x => x.GetQuestion())
                        .ToList()
                }
            },
            _ => new SimpleRound()
            {
                Name = Name,
                Topics = Topics.Select(x => x.GetTopic()).ToList()
            }
        };
    }

    private Task ExecuteNextBlitzQuestion()
    {
        if (_enumerator is null || !_enumerator.MoveNext())
        {
            CurrentQuestion = null;
            return Task.CompletedTask;
        }

        CurrentQuestion = _enumerator.Current;
        return Task.CompletedTask;
    }

    private Task ExecuteAddTopic()
    {
        Topics.Add(new TopicViewModel());
        return Task.CompletedTask;
    }

    private Task ExecuteRemoveTopic(TopicViewModel? topic)
    {
        if (topic is not null && Topics.Contains(topic))
            Topics.Remove(topic);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _enumerator?.Dispose();
    }
}