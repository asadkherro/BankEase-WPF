using System.Xml.Serialization;

namespace BankEase.Models.User
{
    public record UserModel : UserProfileModel
    {
        public UserModel()
        {
            
        }
        public UserModel(UserProfileModel entity) : base(entity)
        {
            
        }
        [XmlElement("Password")]
        public string Password { get; init; }
    }
}
