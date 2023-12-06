using System.Windows;
using System.Windows.Controls;

namespace BrainRing.UI.Common;

public static class DataGridAttachedProperties
{
    public static readonly DependencyProperty CommitOrDiscardProperty =
        DependencyProperty.RegisterAttached(
            "CommitOrDiscard",
            typeof(bool?),
            typeof(DataGridAttachedProperties),
            new PropertyMetadata(null, OnCommitOrDiscard));

    public static bool? GetCommitOrDiscard(DependencyObject dependencyObject)
    {
        return (bool?)dependencyObject.GetValue(CommitOrDiscardProperty);
    }

    public static void SetCommitOrDiscard(DependencyObject dependencyObject, bool? value)
    {
        dependencyObject.SetValue(CommitOrDiscardProperty, value);
    }

    private static void OnCommitOrDiscard(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        switch (e.NewValue)
        {
            case true:
                ((DataGrid)d).CommitEdit();
                ((DataGrid)d).CommitEdit();
                break;
            case false:
                ((DataGrid)d).CancelEdit();
                ((DataGrid)d).CancelEdit();
                break;
            default:
                break;
        }
    }
}