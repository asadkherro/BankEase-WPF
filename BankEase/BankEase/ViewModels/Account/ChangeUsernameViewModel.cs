using BankEase.Core;
using BankEase.Helper;
using BankEase.Models.User;
using BankEase.Repository;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;

namespace BankEase.ViewModels.Account
{
    public class ChangeUsernameViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationCache _cache;
        public ChangeUsernameViewModel(IUserRepository accountRepository, IWindowNavigation nav, ApplicationCache cache) : base(nav)
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
        public RelayCommand ChangeUsernameCommand => new RelayCommand(_ => 
        {
            if (!IsValid(Username))
            {
                ShowDialog.Error("Enter new username.");
                return;
            }
            IsVisible = true; 
        } );

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChange(); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChange(); }
        }
        public RelayCommand ConfirmationCommand => new RelayCommand(_ => 
        {
            IsVisible = true;
            UsernameChangeConfirmation();
        });
        private void UsernameChangeConfirmation() 
        {
            if (!IsValid(Password))
            {
                ShowDialog.Error("Enter old password.");
                return;
            }

            var login = _userRepository.Login(new Models.User.LoginModel() 
            {
                Username = _cache.AppUser.User.Username,
                Password = Password
            });

            if (!login.Success) 
            {
                IsVisible = false;
                ShowDialog.Error("Incorrect password.");
                return;
            }
            var updatedUser = new UserModel(_cache.AppUser.User) { Username = Username , Password = Password };
            var update = _userRepository.UpdateUser(updatedUser, nameof(UserProfileModel.Username), _cache.AppUser.User.Username);

            if (update.Success) 
            {
                ShowDialog.Info("Username has been successfully updated.");
                return;
            }

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
