using BankEase.Models.Transactions;
using BankEase.Repository;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;
using System.Collections.ObjectModel;

namespace BankEase.ViewModels.Admin
{
    public class AdminTransactionViewModel : BaseViewModel
    {
        private readonly ApplicationCache _cache;
        private readonly ITransactionRepository _transactionRepository;
        public AdminTransactionViewModel(IWindowNavigation _nav, ApplicationCache cache, ITransactionRepository transactionRepository) : base(_nav)
        {
            _cache = cache;
            _transactionRepository = transactionRepository;
        }
        public ObservableCollection<TransactionDataModel> Transactions => new (_transactionRepository.GetDetails());

    }
}
