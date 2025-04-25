using System.Xml.Serialization;

namespace BankEase.Models.Transactions
{
    [Serializable]
    public record TransactionModel
    {
        [XmlElement("Amount")]
        public decimal Amount { get; init; }
        [XmlElement("Type")]
        public TransactionType Type { get; init; }
        [XmlElement("From", IsNullable = true)]
        public string? From { get; init; }
        [XmlElement("To", IsNullable = true)]
        public string? To { get; init; }
        [XmlElement("Date")]
        public DateTime Date { get; init; }
        [XmlIgnore]
        public string ColorCode
        {
            get
            {
                return Type == TransactionType.FundsSent || Type == TransactionType.FundsReceive ? "#000000" : "#FFFFFF";
            } 
        }
    }
}
