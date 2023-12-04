using System.Windows;
using System.Windows.Controls;
using BrainRing.Core.Game;
using BrainRing.UI.Common;

namespace BrainRing.UI.Game;

public class RoundDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate SimpleRoundTemplate { get; set; }
    public DataTemplate BlitzRoundTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is not RoundViewModel round)
        {
            return SimpleRoundTemplate;
        }
        
        return round.SelectedRoundType switch
        {
            RoundTypes.SimpleRound => SimpleRoundTemplate,
            RoundTypes.BlitzRound => BlitzRoundTemplate,
            _ => SimpleRoundTemplate
        };
    }
}