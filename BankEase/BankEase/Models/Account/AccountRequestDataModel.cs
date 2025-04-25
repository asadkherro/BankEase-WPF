using BankEase.Models.User;

namespace BankEase.Models.Account
{
    public record AccountRequestDataModel : AccountRequestModel
    {
        public AccountRequestDataModel(AccountRequestModel entity) : base(entity)
        {
            
        }
        public UserProfileModel User { get; init; }
        public AccountModel Account { get; init; }
        public string ColorCode
        {
            get 
            {
                return Type == BankEase.Models.RequestType.AccountUpgrade ? "#FFFFFF" : "#000000";
            }
        }
        public string RequestType 
        {
            get 
            {
                return Type == BankEase.Models.RequestType.AccountUpgrade ? "Account Upgrade " : "Close Account";
            }
        }
    }
}
