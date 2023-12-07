using System.Windows;
using System.Windows.Controls;

namespace BrainRing.UI.SetPlayers;

/// <summary>
/// Interaction logic for SetPlayersView.xaml
/// </summary>
public partial class SetPlayersView : UserControl
{
    public SetPlayersView()
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