using BankEase.ViewModels.Account;
using System.Windows;
using System.Windows.Controls;

namespace BankEase.Views.AccountViews
{
    /// <summary>
    /// Interaction logic for ChangeUsernameView.xaml
    /// </summary>
    public partial class ChangeUsernameView : UserControl
    {
        public ChangeUsernameView(ChangeUsernameViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ChangeUsernameViewModel viewModel && sender is PasswordBox pbox)
            {
                viewModel.Password = pbox.Password;
            }
        }
    }
}
