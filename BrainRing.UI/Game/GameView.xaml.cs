using System.Windows;
using System.Windows.Controls;

namespace BrainRing.UI.Game;

/// <summary>
/// Interaction logic for GameView.xaml
/// </summary>
public partial class GameView : UserControl
{
    public GameView()
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