using System.Xml.Serialization;

namespace BankEase.Models.User
{
    [Serializable]
    public record UserProfileModel
    {
        [XmlElement("Name")]
        public string Name { get; init; }
        [XmlElement("Username")]
        public string Username { get; init; }
        [XmlElement("AccountNumber")]
        public string AccountNumber { get; init; }
        [XmlElement("Role")]
        public UserRole Role { get; init; }
    }
}
