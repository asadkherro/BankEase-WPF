using BankEase.Helper;
using BankEase.Models.Transactions;
using BankEase.Models.User;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;

namespace BankEase.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IXmlService<UserModel> _xmlService;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        public UserRepository(IXmlService<UserModel> xmlService, IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _xmlService = xmlService;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }
        public UserAccountModel GetUserAccount(UserProfileModel userModel)
        {
            var account = _accountRepository.GetUserAccount(userModel);
            return new UserAccountModel() 
            {
                Account = account,
                User = userModel
            };
        }
        public ResponseModel<UserProfileModel> Login(LoginModel model)
        {
            var user = _xmlService.GetElement(nameof(UserModel.Username), model.Username);
            if (user != null) 
            {
                if (user.Password == model.Password)
                    return new ResponseModel<UserProfileModel> { Data = user , Success = true };
            }
            return new ResponseModel<UserProfileModel> { Success = false };
        }
        public ResponseModel<UserModel> Register(UserModel user)
        {
            var usernameExists = UsernameExists(user.Username);
            if(usernameExists)
                return new ResponseModel<UserModel>() { Data = null, Success = false , Message = "Username already exists" };
            var result = _xmlService.Add(user);
            if (result == null) 
            {
                return new ResponseModel<UserModel>() { Data = null, Success = false };
            }
            return new ResponseModel<UserModel>(){ Data = user , Success = true };
        }
        public bool UsernameExists(string username) 
        {
            var user = _xmlService.GetElement(nameof(UserModel.Username), username);
            if (user == null)
                return false;
            return true;
        }
        public ResponseModel<UserModel> GetByUsername(string username)
        {
            var user = _xmlService.GetElement(nameof(UserModel.Username), username);
            if (user != null) 
            {
                return new ResponseModel<UserModel>() { Data = user, Success = true };
            }
            return new ResponseModel<UserModel>() { Data = null, Success = false };
        }
        public ResponseModel<UserProfileModel> UpdateUser(UserModel userModel, string propertyName, string value)
        {
            return new ResponseModel<UserProfileModel>
            {
                Success = true,
                Data = _xmlService.Update(userModel, propertyName, value)
            };
        }

        public List<TransactionModel> GetUserTransactions(string accountNumber)
        {
            return _transactionRepository.GetAll(accountNumber);
        }
        public List<UserProfileModel> GetAllAccounts() 
        {
            return (_xmlService.GetAll()).Select(x=> new UserProfileModel() 
            {
                AccountNumber = x.AccountNumber,
                Name = x.Name,
                Role = x.Role,
                Username = x.Username,
            }).ToList();
        }
    }
}
