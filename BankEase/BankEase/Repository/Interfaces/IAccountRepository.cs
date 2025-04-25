using BankEase.Helper;
using BankEase.Models;
using BankEase.Models.Account;
using BankEase.Models.User;

namespace BankEase.Repository.Interfaces
{
    public interface IAccountRepository
    {
        public ResponseModel<AccountModel> CreateAccount(UserModel userModel, AccountType accountType);
        AccountModel GetUserAccount(UserProfileModel userModel);
        public List<string> GetAccountNumbers(string userAccountNumber);
        ResponseModel<AccountModel> Update(AccountModel updatedModel, string propertyName, string value);
        List<AccountModel> GetAll();
        List<AccountDataModel> GetDetails();
        AccountModel GetAccount(string accountNumber);
    }
}
