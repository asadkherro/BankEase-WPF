using BankEase.Models.Account;

namespace BankEase.Models.User
{
    public record UserAccountModel
    {
        public UserProfileModel User { get; set; }
        public AccountModel Account { get; set; }
    }
}
