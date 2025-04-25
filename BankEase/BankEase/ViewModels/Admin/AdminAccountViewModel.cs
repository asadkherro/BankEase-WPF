using BankEase.Models.Account;
using BankEase.Repository;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;
using System.Collections.ObjectModel;

namespace BankEase.ViewModels.Admin
{
    public class AdminAccountViewModel : BaseViewModel
    {
        private readonly ApplicationCache _cache;
        private readonly IAccountRepository _accountRepository;
        public AdminAccountViewModel(IWindowNavigation nav, ApplicationCache cache, IAccountRepository accountRepository) : base(nav)    
        {
            _cache = cache;
            _accountRepository = accountRepository;
        }
        public ObservableCollection<AccountDataModel> Accounts 
        {
            get 
            {
                return LoadAllAccounts();
            }
        }
        private ObservableCollection<AccountDataModel> LoadAllAccounts() 
        {
            var accounts = _accountRepository.GetDetails();
            return new ObservableCollection<AccountDataModel>(accounts);
        }



    }
}
