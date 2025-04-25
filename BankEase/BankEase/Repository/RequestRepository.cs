using BankEase.Helper;
using BankEase.Models;
using BankEase.Models.Account;
using BankEase.Repository.Interfaces;
using BankEase.Services.Interfaces;

namespace BankEase.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly IXmlService<AccountRequestModel> _xmlService;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        public RequestRepository(IXmlService<AccountRequestModel> xmlService, IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _xmlService = xmlService;
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }
        public ResponseModel<AccountRequestModel> AddRequest(AccountRequestModel model) 
        {
            var result = _xmlService.Add(model);
            if (result != null) 
            {
                return new ResponseModel<AccountRequestModel> { Data = result , Success = true }; 
            }
            return new ResponseModel<AccountRequestModel> { Success = false };
        }
        public ResponseModel<AccountRequestModel> Update(AccountRequestModel model, string accountNumber, RequestStatus status)
        {
            return new ResponseModel<AccountRequestModel>
            {
                Success = true,
                Data = _xmlService.Update(model, nameof(AccountRequestModel.AccountNumber), accountNumber)
            };
        }

        public List<AccountRequestDataModel> GetAll()
        {
            var requests = (_xmlService.GetAll()).Where(x => x.Status == Models.RequestStatus.Pending);
            var requestedAccountNumbers = requests.Select(x=>x.AccountNumber).ToList();
            var accounts = _accountRepository.GetAll();
            accounts = accounts.Where(x => requestedAccountNumbers.Contains(x.AccountNumber)).ToList();
            var userAccounts = _userRepository.GetAllAccounts();
            userAccounts = userAccounts.Where(x => requestedAccountNumbers.Contains(x.AccountNumber)).ToList();

            var response = new List<AccountRequestDataModel>();
            foreach (var request in requests)
            {
                var model = new AccountRequestDataModel(request)
                {
                    User = userAccounts.FirstOrDefault(y => y.AccountNumber == request.AccountNumber),
                    Account = accounts.FirstOrDefault(y => y.AccountNumber == request.AccountNumber)
                };
                response.Add(model);
            }
            return response;
        }

        public AccountRequestModel GetRequest(string accountNumber)
        {
            var element = _xmlService.GetElement(nameof(AccountRequestModel.AccountNumber), accountNumber);
            return element;
        }
    }
}
