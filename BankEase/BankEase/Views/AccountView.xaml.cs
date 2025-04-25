using BankEase.ViewModels.Account;
using System.Windows.Controls;

namespace BankEase.Views
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public AccountView(AccountViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
