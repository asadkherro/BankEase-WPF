using BankEase.Models.Transactions;
using BankEase.Repository;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;
using System.Collections.ObjectModel;

namespace BankEase.ViewModels.Transactions
{
    public class HistoryViewModel : BaseViewModel
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ApplicationCache _cache;
        public HistoryViewModel(ApplicationCache cache, ITransactionRepository transactionRepository, IWindowNavigation nav) : base(nav)
        {
            _transactionRepository = transactionRepository;
            _cache = cache;
        }
        public ObservableCollection<TransactionModel> Transactions
        {
            get => _cache.Transactions;
        }
    }
}
