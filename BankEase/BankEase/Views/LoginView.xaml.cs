using BankEase.Services.Interfaces;
using BankEase.ViewModels.Auth;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BankEase.Views
{
    /// <summary>
    /// Interaction logic for LoginPath.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        IWindowNavigation _nav;
        public LoginView(LoginViewModel viewModel, IWindowNavigation nav)
        {
            InitializeComponent();
            DataContext = viewModel;
            _nav = nav;
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel && sender is PasswordBox pbox)
            {
                viewModel.Password = pbox.Password;
            }
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            _nav.NavigateTo<Register>();
            this.Close();
        }
        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
