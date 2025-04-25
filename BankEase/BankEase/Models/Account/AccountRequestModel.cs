using System.Xml.Serialization;

namespace BankEase.Models.Account
{
    [Serializable]
    public record AccountRequestModel
    {
        [XmlElement("AccountNumber")]
        public string AccountNumber { get; init; }
        [XmlElement("Type")]
        public RequestType Type { get; init; }
        [XmlElement("Status")]
        public RequestStatus Status { get; init; }
        [XmlElement("Date")]
        public DateTime Date { get; init; }
    }
}
