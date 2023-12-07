using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BrainRing.Core.Game;
using CommunityToolkit.Mvvm.Input;
using DynamicData.Binding;

namespace BrainRing.UI.Common;

public class QuestionViewModel : AbstractNotifyPropertyChanged
{
    private bool _isAnswered;
    private QuestionTypes _selectedQuestionType;
    private int _cost;
    private string _description;
    private BitmapImage _bitmapImage;

    public QuestionViewModel(QuestionBase? question = null)
    {
        if (question is null)
        {
            _description = "";
            _cost = 1;
        }
        else
        {
            _description = question.Description;
            _cost = question.Cost;
        }

        if (question is ImageQuestion imageQuestion)
        {
            _selectedQuestionType = QuestionTypes.ImageQuestion;
            _bitmapImage = GetImageFromString(imageQuestion.ImageBase64);
        }
        else
        {
            _selectedQuestionType = QuestionTypes.TextQuestion;
            _bitmapImage = new BitmapImage();
        }

        LoadImageCommand = new AsyncRelayCommand(ExecuteLoadImage);
    }

    public ICommand LoadImageCommand { get; }

    public bool IsAnswered
    {
        get => _isAnswered;
        set => SetAndRaise(ref _isAnswered, value);
    }

    public string Description
    {
        get => _description;
        set => SetAndRaise(ref _description, value);
    }

    public int Cost
    {
        get => _cost;
        set => SetAndRaise(ref _cost, value);
    }
    
    public BitmapImage BitmapImage
    {
        get => _bitmapImage;
        set => SetAndRaise(ref _bitmapImage, value);
    }

    public QuestionTypes SelectedQuestionType
    {
        get => _selectedQuestionType;
        set => SetAndRaise(ref _selectedQuestionType, value);
    }

    public QuestionBase GetQuestion()
    {
        return SelectedQuestionType switch
        {
            QuestionTypes.ImageQuestion => new ImageQuestion
            {
                Description = Description,
                Cost = Cost,
                ImageBase64 = GetStringFromImage(BitmapImage),
            },
            _ => new TextQuestion
            {
                Description = Description,
                Cost = Cost
            }
        };
    }

    public Task Answer(PlayerViewModel? player)
    {
        if (player is not null)
            player.RoundScore += Cost;

        IsAnswered = true;
        return Task.CompletedTask;
    }

    private static BitmapImage GetImageFromString(string str)
    {
        try
        {
            var binaryData = Convert.FromBase64String(str);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(binaryData);
            bitmapImage.EndInit();
            return bitmapImage;
        }
        catch (Exception)
        {
            return new BitmapImage();
        }
    }

    private static string GetStringFromImage(BitmapImage? image)
    {
        try
        {
            if (image is null)
                return "";

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using var stream = new MemoryStream();
            encoder.Save(stream);
            var bytes = stream.ToArray();
            return Convert.ToBase64String(bytes);
        }
        catch (Exception e)
        {
            //todo: добавить уведомление что какая-то картинка не сохранилась
            return "";
        }
    }

    private async Task ExecuteLoadImage()
    {
        var path = await ShowDialogService.SelectImageFilePath();
        if (path is null)
            return;

        var uri = new Uri(path);
        var image = new BitmapImage(uri);
        BitmapImage = image;
    }
}