using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace portfolioBackend.Models
{
    public class PortfolioData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        // 🔗 Owner (recommended for authorization)
        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string UserId { get; set; } = string.Empty;

        // Tagline (hero section)
        [BsonElement("tagline")]
        [Required(ErrorMessage = "Tagline is required")]
        public string Tagline { get; set; } = string.Empty;

        // 🔗 About Me reference (UPDATED)
        [BsonElement("aboutMeId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? AboutMeId { get; set; }

        // 🔗 Project references
        [BsonElement("projectIds")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> ProjectIds { get; set; } = new();

        // 🔗 Achievement references
        [BsonElement("achievementIds")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> AchievementIds { get; set; } = new();

        // 🔗 Skill references
        [BsonElement("skillIds")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> SkillIds { get; set; } = new();

        // 🔗 Contact reference
        [BsonElement("contactMeId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ContactMeId { get; set; }

        // Publish control
        [BsonElement("isPublished")]
        public bool IsPublished { get; set; } = false;

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
