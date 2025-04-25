using BankEase.Core;
using BankEase.Helper;
using BankEase.Models.Account;
using BankEase.Repository;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;

namespace BankEase.ViewModels.Transactions
{
    public class DepositViewModel : BaseViewModel
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ApplicationCache _cache;
        public DepositViewModel(ApplicationCache cache, ITransactionRepository transactionRepository, IWindowNavigation nav) : base(nav)
        {
            _transactionRepository = transactionRepository;
            _cache = cache;
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
        public RelayCommand DespositCommand =>
            new RelayCommand(_ => Desposit());
        private void Desposit()
        {
            if (_amount <= 0)
            {
                ShowDialog.Error("Enter an amount greater than 0.");
                return;
            }
            var transactionModel = new Models.Transactions.TransactionModel()
            {
                Amount = _amount,
                Type = Models.TransactionType.Deposit,
                From = _cache.AppUser.User.AccountNumber,
                To = null,
                Date = DateTime.Now,
            };
            var result = _transactionRepository.Desposit(_cache.AppUser, transactionModel);
            if (result.Success == false)
            {
                ShowDialog.Error();
                return;
            }
            ShowDialog.Success("Account balance has been updated.", "Deposit successful");
            _cache.Transactions.Add(transactionModel);
            _cache.AppUser.Account = _cache.AppUser.Account with { Balance = _cache.AppUser.Account.Balance + _amount };
            _cache.OnPropertyChanged(nameof(AccountModel.Balance));

        }
    }
}
