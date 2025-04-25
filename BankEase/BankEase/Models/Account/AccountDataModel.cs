using BankEase.Models.User;

namespace BankEase.Models.Account
{
    public record AccountDataModel : AccountModel
    {
        public AccountDataModel(AccountModel model) : base(model)
        {
            
        }
        public UserProfileModel User { get; init; }
    }
}
