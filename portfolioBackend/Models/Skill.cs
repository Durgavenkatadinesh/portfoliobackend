using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace portfolioBackend.Models
{
    public class Skill
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [BsonElement("level")]
        public string Level { get; set; } = "Beginner"; // Beginner / Intermediate / Expert
    }
}
