using BankEase.Core;
using BankEase.Helper;
using BankEase.Models;
using BankEase.Models.Account;
using BankEase.Repository;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;

namespace BankEase.ViewModels.Account
{
    public class ServicesViewModel : BaseViewModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ApplicationCache _cache;
        public ServicesViewModel(ApplicationCache cache, IAccountRepository accountRepository, IWindowNavigation nav) : base(nav)
        {
            _accountRepository = accountRepository;
            _cache = cache;

            _creditCardCheck = SetValue(AccountServices.CreditCard);
            _debitCardCheck = SetValue(AccountServices.DebitCard);
            _houseLoanCheck = SetValue(AccountServices.HouseLoan);
            _carLoanCheck = SetValue(AccountServices.CarLoan);
        }
        private bool SetValue(AccountServices services)
        {
            return _cache.AppUser.Account.Service.HasValue && _cache.AppUser.Account.Service.Value.HasFlag(services);
        }

        public bool _creditCardCheck;
        public bool CreditCardCheck
        {
            get => _creditCardCheck;
            set
            {
                _creditCardCheck = value;
                OnPropertyChange();
            }
        }

        private bool _houseLoanCheck;
        public bool HouseLoanCheck
        {
            get => _houseLoanCheck;
            set
            {
                _houseLoanCheck = value;
                OnPropertyChange();
            }
        }

        private bool _debitCardCheck;
        public bool DebitCardCheck
        {
            get => _debitCardCheck;
            set
            {
                _debitCardCheck = value;
                OnPropertyChange();
            }
        }

        private bool _carLoanCheck;
        public bool CarLoanCheck
        {
            get => _carLoanCheck;
            set
            {
                _carLoanCheck = value;
                OnPropertyChange();
            }
        }


        public RelayCommand SaveServicesCommmand => new RelayCommand(_=> SaveServices());
        private void SaveServices() 
        {
            var services = MapServices();
            var model = _cache.AppUser.Account;
            var update = _accountRepository.Update(model with { Service = services }, nameof(AccountModel.AccountNumber), model.AccountNumber);

            if (update.Success) 
            {
                ShowDialog.Success("Thank you for applying to these services.");
                _cache.AppUser.Account = _cache.AppUser.Account with { Service = services };
                return;
            }
            ShowDialog.Error();
        }

        private AccountServices MapServices()
        {
            AccountServices services = 0;

            if (CreditCardCheck)
                services |= AccountServices.CreditCard;

            if (DebitCardCheck)
                services |= AccountServices.DebitCard;

            if (CarLoanCheck)
                services |= AccountServices.CarLoan;

            if (HouseLoanCheck)
                services |= AccountServices.HouseLoan;

            return services;

        }
    }
}
