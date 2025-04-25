using BankEase.Helper;
using BankEase.Models.Transactions;
using BankEase.Models.User;

namespace BankEase.Repository.Interfaces
{
    public interface ITransactionRepository
    {
        ResponseModel<TransactionModel> WithDraw(UserAccountModel user, TransactionModel model);
        ResponseModel<TransactionModel> Desposit(UserAccountModel user, TransactionModel model);
        ResponseModel<TransactionModel> FundsTransfer(UserAccountModel user, TransactionModel senderModel, TransactionModel receiverModel);
        List<TransactionModel> GetAll(string? accountNumber = null);
        List<TransactionDataModel> GetDetails();
    }
}
