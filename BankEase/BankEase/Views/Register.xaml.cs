using BankEase.Services;
using BankEase.Services.Interfaces;
using BankEase.ViewModels.Auth;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BankEase.Views
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private readonly IWindowNavigation _nav;
        public Register(RegisterViewModel RegisterViewModel, IWindowNavigation nav)
        {
            InitializeComponent();
            DataContext = RegisterViewModel;
            _nav = nav;
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel viewModel && sender is PasswordBox pbox) 
            {
                viewModel.Password = pbox.Password;
            }
        }
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            _nav.NavigateTo<LoginView>();
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
