using System.Windows;
using System.Windows.Controls;

namespace BrainRing.UI.Edit;

/// <summary>
/// Interaction logic for RoundEditView.xaml
/// </summary>
public partial class RoundEditView : UserControl
{
    public RoundEditView()
    {
        InitializeComponent();
    }
    
    //взято отсюда https://stackoverflow.com/questions/18281615/deferrefresh-is-not-allowed-during-an-addnew-or-edititem-transaction
    private void FrameworkElement_OnUnloaded(object sender, RoutedEventArgs e)
    {
        if (sender is not DataGrid dataGrid)
            return;

        dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
    }
}