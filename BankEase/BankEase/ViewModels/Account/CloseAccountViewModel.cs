using BankEase.Core;
using BankEase.Helper;
using BankEase.Models.Account;
using BankEase.Repository;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;
using BankEase.Views;
using System.Windows;

namespace BankEase.ViewModels.Account
{
    public class CloseAccountViewModel : BaseViewModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly ApplicationCache _cache;
        public CloseAccountViewModel(ApplicationCache cache, IAccountRepository accountRepository, IUserRepository userRepository, IWindowNavigation nav, IRequestRepository requestRepository) : base(nav)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _cache = cache;
            _requestRepository = requestRepository;
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChange(); }
        }

        public RelayCommand CloseAccountCommand => new RelayCommand(_=> CloseAccount());
        private void CloseAccount() 
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
                ShowDialog.Error("Incorrect password.");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to close your account?",
                "Account Suspension confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.No) 
            {
                return;
            }
            
            if (_cache.AppUser.Account.Type != Models.AccountType.Saving && _cache.AppUser.Account.Balance > 0) 
            {
                ShowDialog.Error($"Your account has balance Rs {_cache.AppUser.Account.Balance.ToString("F2")}. Only Empty accounts can be submitted for suspension.");
                return;
            }

            var updatedAccount = _cache.AppUser.Account with { Status = Models.AccountStatus.Pending };
            var update = _accountRepository.Update(updatedAccount, nameof(AccountModel.AccountNumber), updatedAccount.AccountNumber);

            var requestModel = new AccountRequestModel() { AccountNumber = updatedAccount.AccountNumber , Date = DateTime.Now, Status = Models.RequestStatus.Pending, Type = Models.RequestType.AccountClose };
            var requestRespone = _requestRepository.AddRequest(requestModel);
            if (update.Success && requestRespone.Success)
            {
                ShowDialog.Info("Your account is now closed for operation. It will be suspended on admin's approval. ");
                _cache.Logout();
                NavigationService.NavigateTo<LoginView>();
                NavigationService.CloseWindow<MainWindow>();
                return;
            }
            MessageBox.Show("A problem occured.");
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
