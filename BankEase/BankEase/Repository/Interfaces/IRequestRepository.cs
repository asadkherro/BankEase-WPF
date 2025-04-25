using BankEase.Helper;
using BankEase.Models;
using BankEase.Models.Account;

namespace BankEase.Repository.Interfaces
{
    public interface IRequestRepository
    {
        ResponseModel<AccountRequestModel> AddRequest(AccountRequestModel model);
        AccountRequestModel GetRequest(string accountNumber);
        List<AccountRequestDataModel> GetAll();
        ResponseModel<AccountRequestModel> Update(AccountRequestModel model, string accountNumber, RequestStatus status);
    }
}
