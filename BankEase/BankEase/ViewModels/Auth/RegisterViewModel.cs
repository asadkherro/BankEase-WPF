using BankEase.Core;
using BankEase.Helper;
using BankEase.Models;
using BankEase.Models.User;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;
using BankEase.Views;
using System.Collections.ObjectModel;

namespace BankEase.ViewModels.Auth
{
    public class RegisterViewModel : ObservableObject
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IWindowNavigation _windowNavigation;
        public RegisterViewModel(IUserRepository userRepository, IAccountRepository accountRepository, IWindowNavigation windowNavigation)
        {
            AccountTypes = new ObservableCollection<AccountType>(Enum.GetValues(typeof(AccountType)).Cast<AccountType>());
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _windowNavigation = windowNavigation;
        }
        public RelayCommand RegisterCommand => new RelayCommand(param =>
        {
            if (!ValidateForm()) 
            {
                ShowDialog.Error("All fields are mandatory. ");
                return;
            }

            var userModel = new UserModel()
            {
                Username = Username,
                Name = Fullname,
                Password = Password,
                AccountNumber = Common.GenerateAccountNumber(),
                Role = UserRole.User
            };

            var userAccount = _userRepository.Register(userModel);
            if (!userAccount.Success)
            {
                ShowDialog.Error($"{userAccount.Message}");
                return;
            }
            var bankAccount = _accountRepository.CreateAccount(userModel, AccountType);
            if (!bankAccount.Success)
            {
                ShowDialog.Error($"{userAccount.Message}");
                return;
            }
            ShowDialog.Success($"Account Created. You may now login.");
            _windowNavigation.NavigateTo<LoginView>();
            _windowNavigation.CloseWindow<Register>();
            return;
        });

        private bool ValidateForm() 
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Fullname) || string.IsNullOrEmpty(Password) || AccountType == 0 ) 
            {
                return false;
            }
            return true;
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChange();
            }
        }
        private string _fullname;
        public string Fullname
        {
            get { return _fullname; }
            set
            {
                _fullname = value;
                OnPropertyChange();
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChange();
            }
        }
        public ObservableCollection<AccountType> AccountTypes { get; }
        public AccountType AccountType
        {
            get { return _accountType; }
            set
            {
                _accountType = value;
                OnPropertyChange();
            }
        }
        private AccountType _accountType;
    }
}
