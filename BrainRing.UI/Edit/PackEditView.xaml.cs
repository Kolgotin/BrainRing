using System.Windows;
using System.Windows.Controls;

namespace BrainRing.UI.Edit;

/// <summary>
/// Interaction logic for PackEditView.xaml
/// </summary>
public partial class PackEditView : UserControl
{
    public PackEditView()
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