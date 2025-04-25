using BankEase.ViewModels.Account;
using System.Windows.Controls;

namespace BankEase.Views.AccountViews
{
    /// <summary>
    /// Interaction logic for ServicesView.xaml
    /// </summary>
    public partial class ServicesView : UserControl
    {
        public ServicesView(ServicesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
