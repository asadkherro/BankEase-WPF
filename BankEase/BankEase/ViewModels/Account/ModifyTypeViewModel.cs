using BankEase.Core;
using BankEase.Helper;
using BankEase.Models;
using BankEase.Models.Account;
using BankEase.Repository;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;
using System.Collections.ObjectModel;

namespace BankEase.ViewModels.Account
{
    public class ModifyTypeViewModel : BaseViewModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ApplicationCache _cache;
        public ModifyTypeViewModel(ApplicationCache cache,IAccountRepository accountRepository, IWindowNavigation nav) : base(nav)
        {
            AccountTypes = new ObservableCollection<AccountType>(Enum.GetValues(typeof(AccountType)).Cast<AccountType>());
            _accountRepository = accountRepository;
            _accountRepository = accountRepository;
            _cache = cache;
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
        public RelayCommand ChangeTypeCommand => new RelayCommand(_ => ChangeType() );                                      
        private void ChangeType() 
        {
            if (AccountType == 0) 
            {
                ShowDialog.Error("Choose an account type");
                return;
            }

            if (AccountType == AccountType.Saving && _cache.AppUser.Account.Balance < 500) 
            {
                ShowDialog.Error("You need to have minimum balance of Rs 500 to switch to a saving account.");
                return;
            }

            var updatedModel = _cache.AppUser.Account with 
            { 
                Type = AccountType,
                MinimumBalance = AccountType == AccountType.Saving ? 500 : 0,
            };
            var response = _accountRepository.Update(updatedModel, nameof(AccountModel.AccountNumber), _cache.AppUser.Account.AccountNumber);
            if (response.Success) 
            {
                ShowDialog.Success($"Your account has been changed to a {AccountType} account.");
                _cache.AppUser = _cache.AppUser with { Account = updatedModel };
                _cache.OnPropertyChanged(nameof(AccountModel.Type));
                return;
            }
            ShowDialog.Error();
        }

    }

}
