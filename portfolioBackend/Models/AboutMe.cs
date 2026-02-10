using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace portfolioBackend.Models
{
    public class AboutMe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        

        // Full Name
        [BsonElement("fullName")]
        [Required]
        [MinLength(3)]
        public string FullName { get; set; } = string.Empty;

        // Professional Title
        [BsonElement("professionalTitle")]
        [Required]
        [MinLength(3)]
        public string ProfessionalTitle { get; set; } = string.Empty;

        // Bio / About
        [BsonElement("bio")]
        [Required]
        [MaxLength(1000)]
        public string Bio { get; set; } = string.Empty;

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
