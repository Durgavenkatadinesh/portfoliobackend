using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace portfolioBackend.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        // Used for nikhil.domain.com and /nikhil
        [BsonElement("username")]
        [Required]
        [MinLength(3)]
        public string Username { get; set; } = string.Empty;

        [BsonElement("email")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BsonElement("passwordHash")]
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [BsonElement("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [BsonElement("isSubscribed")]
        public bool IsSubscribed { get; set; } = false;

        [BsonElement("portfolioId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? PortfolioId { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
