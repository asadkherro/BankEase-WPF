using BankEase.ViewModels.Transactions;
using System.Windows.Controls;

namespace BankEase.Views
{
    /// <summary>
    /// Interaction logic for FundsTransferView.xaml
    /// </summary>
    public partial class FundsTransferView : UserControl
    {
        public FundsTransferView(FundsTransferViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
