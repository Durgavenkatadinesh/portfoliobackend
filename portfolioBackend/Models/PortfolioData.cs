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
         
        [BsonElement("tagline")]
        [Required(ErrorMessage = "Tagline is required")]
        public string Tagline { get; set; } = string.Empty;


        // Project references
        [BsonElement("projectIds")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> ProjectIds { get; set; } = new();

        // About Me
        [BsonElement("aboutMe")]
        [Required]
        public string AboutMe { get; set; } = string.Empty;

        // Achievement references
        [BsonElement("achievementIds")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> AchievementIds { get; set; } = new();

        // Skill references
        [BsonElement("skillIds")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> SkillIds { get; set; } = new();

        // Contact reference
        [BsonElement("contactMeId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string ContactMeId { get; set; } = string.Empty;
    }
}
