namespace BankEase.Models.User
{
    public record LoginModel
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
