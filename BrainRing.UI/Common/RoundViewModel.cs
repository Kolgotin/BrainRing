using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BrainRing.Core.Game;
using CommunityToolkit.Mvvm.Input;
using DynamicData.Binding;

namespace BrainRing.UI.Common;

public class RoundViewModel : AbstractNotifyPropertyChanged
{
    private string _name;
    
    public RoundViewModel(Round? round = null)
    {
        round ??= new Round();
        _name = round.Name;
        Topics = new ObservableCollection<TopicViewModel>(
            round.Topics
                .Select(x => new TopicViewModel(x))
                .ToList());
        AddTopicCommand = new AsyncRelayCommand(ExecuteAddTopic);
        RemoveQuestionCommand = new AsyncRelayCommand<TopicViewModel>(ExecuteRemoveQuestion);
    }

    public ICommand AddTopicCommand { get; }
    public ICommand RemoveQuestionCommand { get; }

    public string Name
    {
        get => _name;
        set => SetAndRaise(ref _name, value);
    }

    public ObservableCollection<TopicViewModel> Topics { get; }

    public Round GetRound() =>
        new()
        {
            Name = Name,
            Topics = Topics.Select(x => x.GetTopic()).ToList()
        };

    private Task ExecuteAddTopic()
    {
        Topics.Add(new TopicViewModel());
        return Task.CompletedTask;
    }

    private Task ExecuteRemoveQuestion(TopicViewModel? topic)
    {
        if(topic is not null && Topics.Contains(topic))
            Topics.Remove(topic);
        return Task.CompletedTask;
    }
}