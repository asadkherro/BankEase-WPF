using BankEase.ViewModels.Transactions;
using System.Windows.Controls;

namespace BankEase.Views
{
    /// <summary>
    /// Interaction logic for HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        public HistoryView(HistoryViewModel historyView)
        {
            InitializeComponent();
            DataContext = historyView;
        }
    }
}
