using System.Threading.Tasks;
using BrainRing.Core.Game;
using DynamicData.Binding;
using static System.Formats.Asn1.AsnWriter;

namespace BrainRing.UI.Common;

//todo: редактирование вопроса с выбором типа: обычный, фото, (в дальнейшем аудио)
public class QuestionViewModel : AbstractNotifyPropertyChanged
{
    private bool _isAnswered;
    
    public QuestionViewModel(Question? question = null)
    {
        Question = question ?? new Question();
    }

    public Question Question { get; }

    public bool IsAnswered
    {
        get => _isAnswered;
        set => SetAndRaise(ref _isAnswered, value);
    }

    public string Description
    {
        get => Question.Description;
        set
        {
            if (value == Question.Description) return;
            Question.Description = value;
            OnPropertyChanged();
        }
    }

    public int Cost
    {
        get => Question.Cost;
        set
        {
            if (value == Question.Cost) return;
            Question.Cost = value;
            OnPropertyChanged();
        }
    }

    public Task Answer(PlayerViewModel? player)
    {
        if (player is not null)
            player.Score += Cost;

        IsAnswered = true;
        return Task.CompletedTask;
    }
}