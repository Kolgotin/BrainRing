using System.Windows;
using System.Windows.Controls;

namespace BrainRing.UI.Game;

/// <summary>
/// Interaction logic for RoundView.xaml
/// </summary>
public partial class RoundView : UserControl
{
    public RoundView()
    {
        InitializeComponent();
    }

    private void FrameworkElement_OnUnloaded(object sender, RoutedEventArgs e)
    {
        if (sender is not DataGrid dataGrid)
            return;

        dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
    }
}