using BankEase.Models;
using BankEase.Models.Account;
using BankEase.Models.Transactions;
using BankEase.Repository;
using BankEase.Services.Interfaces;
using System.Collections.ObjectModel;

namespace BankEase.ViewModels.Main
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly ApplicationCache _cache;
        public HomeViewModel(IWindowNavigation nav, ApplicationCache cache) : base(nav)
        {
            _cache = cache;
            _cache.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_cache.AppUser.Account.Type))
                {
                    OnPropertyChange(nameof(AccountModel.Type));
                }

                if (e.PropertyName == nameof(_cache.AppUser.Account.Balance))
                {
                    OnPropertyChange(nameof(AccountModel.Balance));
                }
            };
        }
        public AccountType Type => _cache.AppUser.Account.Type;
        public string AccountNumber => _cache.AppUser.Account.AccountNumber;
        public decimal Balance => _cache.AppUser.Account.Balance;
        public ObservableCollection<TransactionModel> Transactions
        {
            get => GetRecentTransactions(_cache.Transactions);
        }
        private ObservableCollection<TransactionModel> GetRecentTransactions(ObservableCollection<TransactionModel> list) 
        {
            if (list == null || list.Count == 0)
                return new ObservableCollection<TransactionModel>();

            var recentTransactions = list
                .OrderByDescending(transaction => transaction.Date)
                .Take(4)
                .ToList();
            return new ObservableCollection<TransactionModel>(recentTransactions);
        }
    }
}
