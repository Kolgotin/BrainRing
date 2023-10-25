using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BrainRing.Core.Game;
using CommunityToolkit.Mvvm.Input;
using DynamicData.Binding;

namespace BrainRing.UI.Common;

public class TopicViewModel : AbstractNotifyPropertyChanged
{
    private string _name;

    public TopicViewModel(Topic? topic = null)
    {
        topic ??= new Topic();
        _name = topic.Name;
        Questions = new ObservableCollection<QuestionViewModel>(
            topic.Questions
                .Select(x => new QuestionViewModel(x))
                .ToList());
        AddQuestionCommand = new AsyncRelayCommand(ExecuteAddQuestion);
        RemoveQuestionCommand = new AsyncRelayCommand<QuestionViewModel>(ExecuteRemoveQuestion);
    }

    public ICommand AddQuestionCommand { get; }
    public ICommand RemoveQuestionCommand { get; }

    public string Name
    {
        get => _name;
        set => SetAndRaise(ref _name, value);
    }

    public ObservableCollection<QuestionViewModel> Questions { get; }

    public Topic GetTopic() =>
        new()
        {
            Name = Name,
            Questions = Questions.Select(x => x.Question).ToList()
        };

    private Task ExecuteAddQuestion()
    {
        Questions.Add(new QuestionViewModel());
        return Task.CompletedTask;
    }

    private Task ExecuteRemoveQuestion(QuestionViewModel? question)
    {
        if (question is not null && Questions.Contains(question))
            Questions.Remove(question);
        return Task.CompletedTask;
    }
}