using BankEase.Core;
using BankEase.Helper;
using BankEase.Models.Account;
using BankEase.Models.Transactions;
using BankEase.Repository;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;
using System.Collections.ObjectModel;

namespace BankEase.ViewModels.Transactions
{
    public class FundsTransferViewModel : BaseViewModel
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ApplicationCache _cache;
        public FundsTransferViewModel(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IWindowNavigation nav, ApplicationCache cache) : base(nav)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _cache = cache;
            AccountNumbers = LoadAccountNumbers();
        }
        public ObservableCollection<string> AccountNumbers { get; set; }
        private ObservableCollection<string> LoadAccountNumbers()
        {
            var result = _accountRepository.GetAccountNumbers(_cache.AppUser.Account.AccountNumber);
            result = result.Where(x => x != _cache.AppUser.Account.AccountNumber).ToList();
            return new ObservableCollection<string>(result);
        }
        private string _accountNumber;
        public string AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; OnPropertyChange(); }
        }
        private int _amount;
        public string Amount
        {
            get => _amount.ToString();
            set
            {
                if (int.TryParse(value, out int result))
                    _amount = result;
                else
                    _amount = 0;
                OnPropertyChange();
            }
        }
        public RelayCommand TransferCommand =>
            new RelayCommand(_ => Transfer());

        private void Transfer()
        {   

            if (string.IsNullOrEmpty(AccountNumber)) 
            {
                ShowDialog.Error("Choose an account to transfer funds.");
                return;
            }
            if (_amount <= 0)
            {
                ShowDialog.Error("Enter an amount greater than 0.");
                return;
            }

            var receverAccountNumber = ExtractAccountNumber(AccountNumber);
            var senderTransactionModel = new TransactionModel()
            {
                Amount = _amount,
                Date = DateTime.Now,
                From = _cache.AppUser.User.AccountNumber,
                Type = Models.TransactionType.FundsSent,
            };
            var receiverTransactionModel = new TransactionModel()
            {
                Amount = _amount,
                Date = DateTime.Now,
                From = _cache.AppUser.User.AccountNumber,
                To = receverAccountNumber,
                Type = Models.TransactionType.FundsReceive,
            };
            var result = _transactionRepository.FundsTransfer(_cache.AppUser, senderTransactionModel, receiverTransactionModel);
            if (!result.Success)
            {
                ShowDialog.Error(result.Message);
                return;
            }
            _cache.AppUser.Account = _cache.AppUser.Account with { Balance = _cache.AppUser.Account.Balance - _amount };
            _cache.Transactions.Add(senderTransactionModel);
            _cache.OnPropertyChanged(nameof(AccountModel.Balance));
            ShowDialog.Success("Transaction has been successful.");
            return;
        }
        private string ExtractAccountNumber(string input)
        {
            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return parts[^1];
        }

    }
}
