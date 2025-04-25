using BankEase.Helper;
using BankEase.Models;
using BankEase.Models.Account;
using BankEase.Models.User;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;

namespace BankEase.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IXmlService<AccountModel> _xmlService;
        private readonly IXmlService<UserModel> _xmlUserService;
        public AccountRepository(IXmlService<AccountModel> xmlService , IXmlService<UserModel> xmlUserService)
        {
            _xmlService = xmlService;
            _xmlUserService = xmlUserService;
        }
        public ResponseModel<AccountModel> Update(AccountModel updatedModel, string propertyName, string value)
        {
            return new ResponseModel<AccountModel> { Success = true ,
                Data = _xmlService.Update(updatedModel, propertyName, value)
            };
        }
        public AccountModel GetAccount(string accountNumber) 
        {
            return _xmlService.GetElement(nameof(AccountModel.AccountNumber), accountNumber);
        }
        public ResponseModel<AccountModel> CreateAccount(UserModel userModel, AccountType accountType) 
        {
            var account = new AccountModel() 
            {
                AccountNumber = userModel.AccountNumber,
                Balance = accountType == AccountType.Saving ? 500 : 0,
                Type  = accountType,
                Service = 0,
                Status = AccountStatus.Open,
                MinimumBalance = accountType == AccountType.Saving ? 500 : 0,
            };
            var response = _xmlService.Add(account);
            if (response == null) 
            {
                return new ResponseModel<AccountModel>() { Success = false };
            }
            return new ResponseModel<AccountModel>() { Success = true, Data = response };
        }

        public List<string> GetAccountNumbers(string userAccountNumber)
        {
            var userAccounts = _xmlUserService.GetAll();
            var adminAccounts = userAccounts.Where(x => x.Role == UserRole.Admin);
            var adminAccountNumbers = adminAccounts.Select(x => x.AccountNumber).ToList();
            var accounts = (_xmlService.GetAll()).Where(x=> x.Status == AccountStatus.Open && !adminAccountNumbers.Contains(x.AccountNumber) && x.AccountNumber != userAccountNumber);
            return accounts.Select(x=> $"{GetName(x, userAccounts)} {x.AccountNumber}" ).ToList();
        }
        private string GetName(AccountModel accountModel, List<UserModel> model) 
        {
            return model.FirstOrDefault(y => y.AccountNumber == accountModel.AccountNumber)?.Name ?? "";
        }

        public AccountModel GetUserAccount(UserProfileModel userModel)
        {
            return _xmlService.GetElement(nameof(AccountModel.AccountNumber), userModel.AccountNumber);
        }

        public List<AccountModel> GetAll()
        {
            var userAccounts = _xmlUserService.GetAll();
            var adminAccounts = userAccounts.Where(x => x.Role == UserRole.Admin);
            var adminAccountNumbers = adminAccounts.Select(x => x.AccountNumber).ToList();
            return (_xmlService.GetAll()).Where(x => !adminAccountNumbers.Contains(x.AccountNumber)).ToList();
        }
        public List<AccountDataModel> GetDetails()
        {
            var accounts = GetAll();
            var accountNumbers = accounts.Select(x => x.AccountNumber);
            var users = _xmlUserService.GetAll();
            users = users.Where(y => accountNumbers.Contains(y.AccountNumber)).ToList();
            var result = new List<AccountDataModel>();
            foreach(var account in accounts) 
            {
                var model = new AccountDataModel(account)
                {
                    User = users.FirstOrDefault(x=> x.AccountNumber == account.AccountNumber)
                };
                result.Add(model);
            }
            return result;
        }
    }
}
