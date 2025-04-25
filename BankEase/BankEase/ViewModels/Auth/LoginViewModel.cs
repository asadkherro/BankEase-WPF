using BankEase.Core;
using BankEase.Helper;
using BankEase.Models.Transactions;
using BankEase.Repository;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;
using BankEase.Views;

namespace BankEase.ViewModels.Auth
{
    public class LoginViewModel : ObservableObject
    {
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWindowNavigation _nav;
        private readonly ApplicationCache _cache;
        public LoginViewModel(IUserRepository userRepository, IWindowNavigation nav, ApplicationCache cache, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _nav = nav;
            _cache = cache;
            _transactionRepository = transactionRepository;
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChange(nameof(Username));
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChange(nameof(Password));
            }
        }
        public RelayCommand LoginCommand => new RelayCommand(param =>
        {
            if (!ValidateForm())
            {
                ShowDialog.Error("All fields are mandatory.");
                return;
            }
            var response = _userRepository.Login(new Models.User.LoginModel() { Password = Password, Username = Username });
            if (response.Success == true)
            {
                var userAccount = _userRepository.GetUserAccount(response.Data);
                if (userAccount.Account.Status == Models.AccountStatus.Pending) 
                {
                    ShowDialog.Error("Your account has been suspended for use. Waiting for admin approval.");
                    return;
                }
                if (userAccount.Account.Status == Models.AccountStatus.Closed)
                {
                    ShowDialog.Error("Your account has been closed. ");
                    return;
                }
                var transactions = new List<TransactionModel>();
                if (userAccount.User.Role == Models.UserRole.Admin)
                    transactions = _transactionRepository.GetAll();
                else
                    transactions = _userRepository.GetUserTransactions(userAccount.Account.AccountNumber);
                _cache.AppUser = userAccount;
                transactions = transactions.OrderByDescending(x => x.Date).ToList();
                _cache.Transactions = new(transactions);
                _nav.NavigateTo<MainWindow>();
                _nav.CloseWindow<LoginView>();
                return;
            }
            ShowDialog.Error("Incorrect username or password", "Sign In failed");
            return;
        });
        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return false;
            }
            return true;
        }
    }
}
