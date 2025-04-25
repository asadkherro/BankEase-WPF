using BankEase.Models.User;

namespace BankEase.Models.Transactions
{
    public record TransactionDataModel : TransactionModel
    {
        public TransactionDataModel(TransactionModel model) : base(model)
        {
            
        }
        public UserProfileModel Sender { get; init; }
        public UserProfileModel Receiver { get; init; }
        public string Name 
        {
            get 
            {
                if (Type == TransactionType.FundsReceive && Receiver != null)
                    return Receiver.Name;
                return Sender.Name;
            }
        }

    }
}
