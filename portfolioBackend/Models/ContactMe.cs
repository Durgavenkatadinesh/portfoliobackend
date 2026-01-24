using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace portfolioBackend.Models
{
    public class ContactMe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("email")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BsonElement("phoneNumber")]
        [RegularExpression(@"^[6-9]\d{9}$")]
        public string PhoneNumber { get; set; } = string.Empty;

        [BsonElement("linkedin")]
        public string LinkedIn { get; set; } = string.Empty;

        [BsonElement("github")]
        public string GitHub { get; set; } = string.Empty;

        [BsonElement("twitter")]
        public string Twitter { get; set; } = string.Empty;
    }
}
