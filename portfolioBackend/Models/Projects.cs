using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace portfoliobackend.Models
{
    public class Project : IValidatableObject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("password")]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = string.Empty;

        [BsonElement("isSubscribed")]
        public bool IsSubscribed { get; set; } = false;

        [BsonElement("phoneNumber")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }

        [BsonElement("email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        // 🔗 Optional reference (can be null at login)
        [BsonElement("portfolioDataId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? PortfolioDataId { get; set; }

        // Custom validation: either PhoneNumber or Email is mandatory
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber) &&
                string.IsNullOrWhiteSpace(Email))
            {
                yield return new ValidationResult(
                    "Either phone number or email must be provided",
                    new[] { nameof(PhoneNumber), nameof(Email) }
                );
            }
        }
    }
}
