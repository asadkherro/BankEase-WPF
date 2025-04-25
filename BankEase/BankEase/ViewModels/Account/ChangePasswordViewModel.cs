using BankEase.Core;
using BankEase.Helper;
using BankEase.Models.User;
using BankEase.Repository;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;
using BankEase.Views;

namespace BankEase.ViewModels.Account
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationCache _cache;
        public ChangePasswordViewModel(IUserRepository accountRepository, IWindowNavigation nav, ApplicationCache cache) : base(nav)
        {
            _userRepository = accountRepository;
            _cache = cache;
        }
        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; OnPropertyChange(); }
        }
        private string _oldPassword;
        public string OldPassword
        {
            get { return _oldPassword; }
            set { _oldPassword = value; OnPropertyChange(); }
        }
        public RelayCommand PasswordConfirmationCommand => new RelayCommand(_ => PasswordConfirmation() );
        private void PasswordConfirmation() 
        {
            if (!IsValid(OldPassword))
            {
                ShowDialog.Error("Enter old password.");
                return;
            }
            var login = _userRepository.Login(new Models.User.LoginModel() 
            {
                Username = _cache.AppUser.User.Username,
                Password = OldPassword
            });
            if (!login.Success)
            {
                ShowDialog.Error("Incorrect password.");
                return;
            }
            IsVisible = true;
        }
        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; OnPropertyChange(); }
        }
        public RelayCommand ChangePasswordCommand => new RelayCommand(_ => ChangePassword());
        private void ChangePassword()
        {
            if (!IsValid(NewPassword)) 
            {
                ShowDialog.Error("Enter new password.");
                return ;
            }

            var updateUserModel = new UserModel(_cache.AppUser.User) 
            {
                Password = NewPassword,
            };
            var update = _userRepository.UpdateUser(updateUserModel, nameof(UserModel.Username), updateUserModel.Username);
            if (!update.Success)
            {
                ShowDialog.Error();
                return;
            }
            ShowDialog.Info("Password has been successfully changed. You may now login.");
            _cache.Logout();
            NavigationService.NavigateTo<LoginView>();
            NavigationService.CloseWindow<MainWindow>();
            IsVisible = false;
        }
        private bool IsValid(string field)
        {
            if (string.IsNullOrEmpty(field)) 
            {
                return false;
            }
            return true;
        }
    }
}
