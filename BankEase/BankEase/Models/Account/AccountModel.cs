using System.Xml.Serialization;

namespace BankEase.Models.Account
{
    [Serializable]
    public record AccountModel
    {
        [XmlElement("AccountNumber")]
        public string AccountNumber { get; init; }
        [XmlElement("Balance")]
        public decimal Balance { get; init; }
        [XmlElement("Type")]
        public AccountType Type { get; init; }
        [XmlElement("Service", IsNullable = true)]
        public AccountServices? Service { get; init; }
        [XmlElement("Status")]
        public AccountStatus Status { get; init; }
        [XmlElement("MinimumBalance")]
        public decimal MinimumBalance { get; init; }
    }
}
