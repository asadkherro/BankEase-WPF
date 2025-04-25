using BankEase.Helper;
using BankEase.Models.Transactions;
using BankEase.Models.User;

namespace BankEase.Repository.Interfaces
{
    public interface IUserRepository
    {
        public ResponseModel<UserModel> Register(UserModel user);
        public ResponseModel<UserProfileModel> Login(LoginModel model);
        ResponseModel<UserModel> GetByUsername(string username);
        UserAccountModel GetUserAccount(UserProfileModel userModel);
        List<TransactionModel> GetUserTransactions(string accountNumber);
        ResponseModel<UserProfileModel> UpdateUser(UserModel userModel, string propertyName, string value);
        List<UserProfileModel> GetAllAccounts();
    }
}
