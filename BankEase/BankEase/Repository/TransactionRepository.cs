using BankEase.Helper;
using BankEase.Models.Account;
using BankEase.Models.Transactions;
using BankEase.Models.User;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;
namespace BankEase.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IXmlService<TransactionModel> _xmlService;
        private readonly IXmlService<AccountModel> _xmlAccountService;
        private readonly IXmlService<UserModel> _xmlUserService;
        public TransactionRepository(IXmlService<TransactionModel> xmlService, IXmlService<AccountModel> xmlAccountService, IXmlService<UserModel> xmlUserService)
        {
            _xmlService = xmlService;
            _xmlAccountService = xmlAccountService;
            _xmlUserService = xmlUserService;
        }
        public List<TransactionModel> GetAll(string? accountNumber = null)
        {
            var transactions = (_xmlService.GetAll()).Where(x => string.IsNullOrEmpty(accountNumber) || (x.To == accountNumber || x.From == accountNumber)).ToList();
            if (!string.IsNullOrEmpty(accountNumber)) 
            {
                transactions = transactions.Where(x => !(x.Type == Models.TransactionType.FundsReceive && x.From == accountNumber)).ToList();
            }
            return transactions;
        }
        public ResponseModel<TransactionModel> Desposit(UserAccountModel user, TransactionModel model)
        {
            var updateAccount = user.Account with { Balance = user.Account.Balance + model.Amount };
            _xmlAccountService.Update(updateAccount, nameof(AccountModel.AccountNumber), user.Account.AccountNumber);
            var res = _xmlService.Add(model);  
            return new ResponseModel<TransactionModel> {  Data = res , Success = true };
        }

        public ResponseModel<TransactionModel> FundsTransfer(UserAccountModel user, TransactionModel senderModel, TransactionModel receiverModel)
        {
            if (user.Account.Type == Models.AccountType.Saving)
            {
                var newBalance = user.Account.Balance - senderModel.Amount;
                if (newBalance < user.Account.MinimumBalance)
                {
                    return new ResponseModel<TransactionModel> { Data = senderModel, Success = false , Message = "You have in sufficent funds to complete the transfer." };
                }
            }
            if (user.Account.Balance < senderModel.Amount)
            {
                return new ResponseModel<TransactionModel> { Message = "You have in sufficent funds to complete the transfer." , Data = senderModel, Success = false };
            }

            //update recevier balance
            var receiverAccount = _xmlAccountService.GetElement(nameof(AccountModel.AccountNumber), receiverModel.To);
            _xmlAccountService.Update(receiverAccount with { Balance = receiverAccount.Balance + senderModel.Amount }, nameof(AccountModel.AccountNumber), receiverModel.To);
            _xmlService.Add(receiverModel);

            // Update sender balance
            _xmlAccountService.Update(user.Account with { Balance = user.Account.Balance - senderModel.Amount }, nameof(AccountModel.AccountNumber), user.Account.AccountNumber);
            _xmlService.Add(senderModel);

            return new ResponseModel<TransactionModel> { Message = "Transaction completed.", Data = senderModel, Success = true };
        }

        public ResponseModel<TransactionModel> WithDraw(UserAccountModel user, TransactionModel model)
        {
            if (user.Account.Type == Models.AccountType.Saving) 
            {
                var newBalance = user.Account.Balance - model.Amount;
                if (newBalance < user.Account.MinimumBalance) 
                {
                    return new ResponseModel<TransactionModel> { Data = model, Success = false , Message = "Balance cannot be less than saving account limit." };
                }
            }
            if (user.Account.Balance < model.Amount) 
            {
                return new ResponseModel<TransactionModel> { Data = model, Success = false , Message = "Insufficient funds." };
            }
            var updateAccount = user.Account with { Balance = user.Account.Balance - model.Amount };
            _xmlAccountService.Update(updateAccount, nameof(AccountModel.AccountNumber), user.Account.AccountNumber);
            var res = _xmlService.Add(model);
            return new ResponseModel<TransactionModel> { Data = res, Success = true };
        }
        public List<TransactionDataModel> GetDetails() 
        {
            var transactions = GetAll();
            var users = _xmlUserService.GetAll();
            var result = new List<TransactionDataModel>();
            foreach (var transaction in transactions)
            {
                var sender = new UserProfileModel();
                var receiver = new UserProfileModel();
                if (!string.IsNullOrEmpty(transaction.From))
                {
                    sender = _xmlUserService.GetElement(nameof(AccountModel.AccountNumber) ,transaction.From);
                }
                if (!string.IsNullOrEmpty(transaction.To))
                {
                    receiver = _xmlUserService.GetElement(nameof(AccountModel.AccountNumber), transaction.To);
                }
                var model = new TransactionDataModel(transaction)
                {
                    Sender = sender,
                    Receiver = receiver,
                };
                result.Add(model);
            }
            return result;
        }
    }
}
