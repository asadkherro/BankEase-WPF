using BankEase.Core;
using BankEase.Helper;
using BankEase.Models;
using BankEase.Models.Account;
using BankEase.Repository;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace BankEase.ViewModels.Admin
{
    public class AdminHomeViewModel : BaseViewModel
    {
        private readonly ApplicationCache _cache;
        private readonly IAccountRepository _accountRepository;
        private readonly IRequestRepository _requestRepository;
        public AdminHomeViewModel(IWindowNavigation navigation, ApplicationCache cache, IAccountRepository accountRepository, IRequestRepository requestRepository) : base(navigation)
        {
            _cache = cache;
            _accountRepository = accountRepository;
            _requestRepository = requestRepository;
        }
        public RelayCommand ApproveRequestCommand => new RelayCommand(param => ApproveRequest(param));
        private void ApproveRequest(object param) 
        {
            if (param is string accountNumber) 
            {
                MessageBoxResult result = MessageBox.Show(
                "Cancel user account?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
                var account = _accountRepository.GetAccount(accountNumber);
                var requestModel = _requestRepository.GetRequest(accountNumber);
                if (result == MessageBoxResult.Yes)
                {
                    var res = _requestRepository.Update(requestModel with { Status = RequestStatus.Approved }, accountNumber, RequestStatus.Approved);
                    if (!res.Success) 
                    {
                        ShowDialog.Error();
                        return;
                    }
                    var accountStatusUpdate = _accountRepository.Update(account with { Status = AccountStatus.Closed }, nameof(AccountModel.AccountNumber), accountNumber);
                    ShowDialog.Success("Account has been closed");
                }
                else if (result == MessageBoxResult.No)
                {
                    var res = _requestRepository.Update(requestModel with { Status = RequestStatus.Rejected }, accountNumber, RequestStatus.Rejected);
                    if (!res.Success)
                    {
                        ShowDialog.Error();
                        return;
                    }
                    var accountStatusUpdate = _accountRepository.Update(account with { Status = AccountStatus.Open }, nameof(AccountModel.AccountNumber), accountNumber);
                    ShowDialog.Success("Account's suspension has been removed. ");
                }
                var requestToRemove = AccountRequests.FirstOrDefault(x => x.AccountNumber == accountNumber);
                if (requestToRemove != null)
                {
                    AccountRequests.Remove(requestToRemove);
                    _accountRequests.Remove(requestToRemove);
                }
            }
        }
        public List<AccountModel> UserAccounts => _accountRepository.GetAll();
        private ObservableCollection<AccountRequestDataModel> _accountRequests;
        public ObservableCollection<AccountRequestDataModel> AccountRequests
        {
            get
            {
                if (_accountRequests == null)
                {
                    _accountRequests = new ObservableCollection<AccountRequestDataModel>(_requestRepository.GetAll());
                }
                return _accountRequests;
            }
            set
            {
                _accountRequests = value;
            }
        }
        public string Active
        {
            get 
            {
                return "Active " + UserAccounts.Count(x => x.Status == Models.AccountStatus.Open);
            }
        }
        public string InActive
        {
            get
            {
                return "InActive " + UserAccounts.Count(x => x.Status != Models.AccountStatus.Open);
            }
        }

        public string TotalTransactions
        {
            get 
            {
                return "Total " + _cache.Transactions.Count();
            }
        }

        public string Deposit
        {
            get
            {
                return "Deposit " + _cache.Transactions.Count(x => x.Type == Models.TransactionType.Deposit);
            }
        }

        public string Withdrawl
        {
            get
            {
                return "Withdrawl " + _cache.Transactions.Count(x => x.Type == Models.TransactionType.Withdrawl);
            }
        }

        public string FundsTransfer
        {
            get
            {
                return "Funds Transfer " + _cache.Transactions.Count(x => x.Type == Models.TransactionType.FundsReceive || x.Type == Models.TransactionType.FundsSent);
            }
        }
    }

}
