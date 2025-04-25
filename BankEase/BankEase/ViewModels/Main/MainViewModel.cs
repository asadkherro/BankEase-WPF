using BankEase.Core;
using BankEase.Repository;
using BankEase.Services.Interfaces;
using BankEase.Views;
using BankEase.Views.AdminViews;
using System.Windows;

namespace BankEase.ViewModels.Main
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ApplicationCache _cache;
        public MainViewModel(IWindowNavigation nav, ApplicationCache cache) : base(nav)
        {
            _cache = cache;
            CurrentView = _cache.AppUser.User.Role == Models.UserRole.User ?  DisplayView<HomeView>() : DisplayView<AdminHomeView>();
        }
        public RelayCommand HomeCommand => new RelayCommand(_ => 
        {
            if (_cache.AppUser.User.Role == Models.UserRole.User)
                CurrentView = DisplayView<HomeView>();
            else
                CurrentView = DisplayView<AdminHomeView>();
        });
        public RelayCommand TransactionCommand => new RelayCommand(_ => 
        {
            if (_cache.AppUser.User.Role == Models.UserRole.User)
                CurrentView = DisplayView<TransactionView>();
            else
                CurrentView = DisplayView<AdminTransactionView>();
        });
        public RelayCommand AccountCommand => new RelayCommand(_ => 
        {
            if (_cache.AppUser.User.Role == Models.UserRole.User)
                CurrentView = DisplayView<AccountView>();
            else
                CurrentView = DisplayView<AdminAccountView>();
        });
        public RelayCommand LogoutCommand => new RelayCommand(_ =>
        {
            _cache.Logout();
            NavigationService.NavigateTo<LoginView>();
            NavigationService.CloseWindow<MainWindow>();
        });
        public RelayCommand ExitCommand => new RelayCommand(_ =>
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to exit?",
                "Exit",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                _cache.Logout();
                NavigationService.CloseWindow<MainWindow>();
            }
        });
        public string FullName => "Welcome " + _cache.AppUser.User.Name;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChange(); }
        }
        private object _currentView;
    }
}
