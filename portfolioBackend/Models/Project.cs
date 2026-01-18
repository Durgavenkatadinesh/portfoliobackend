using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace portfolioBackend.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        [Required, MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [BsonElement("tagline")]
        [Required]
        public string Tagline { get; set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("githubUrl")]
        public string GithubUrl { get; set; } = string.Empty;

        [BsonElement("liveUrl")]
        public string LiveUrl { get; set; } = string.Empty;
    }
}
