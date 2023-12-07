using System.Windows;
using System.Windows.Controls;

namespace BrainRing.UI.Finish;

/// <summary>
/// Interaction logic for FinishViewModel.xaml
/// </summary>
public partial class FinishView : UserControl
{
    public FinishView()
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