using BankEase.Core;
using BankEase.Helper;
using BankEase.Models.Account;
using BankEase.Models.Transactions;
using BankEase.Repository;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;

namespace BankEase.ViewModels.Transactions
{
    public class WithdrawViewModel : BaseViewModel
    {
        private readonly ITransactionRepository _transactionRepository;
        private ApplicationCache _cache;
        public WithdrawViewModel(ITransactionRepository transactionRepository, IWindowNavigation nav, ApplicationCache cache) : base(nav)
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
        public RelayCommand WithDrawCommand =>
            new RelayCommand(_ => Withdraw());

        private void Withdraw()
        {

            if (_amount <= 0) 
            {
                ShowDialog.Error("Enter an amount greater than 0.");
                return;
            }

            var transactionModel = new TransactionModel() 
            {
                Amount = _amount,
                Type = Models.TransactionType.Withdrawl,
                From = _cache.AppUser.User.AccountNumber,
                To = null,
                Date = DateTime.Now,
            };
            var result = _transactionRepository.WithDraw(_cache.AppUser, transactionModel);
            if (result.Success == false)
            {
                ShowDialog.Error(result.Message);
                return;
            }
            _cache.Transactions.Add(transactionModel);
            _cache.AppUser.Account = _cache.AppUser.Account with { Balance = _cache.AppUser.Account.Balance - _amount };
            _cache.OnPropertyChanged(nameof(AccountModel.Balance));
            ShowDialog.Success("Account balance has been updated.", "Withdraw successful");
        }
    }

}
