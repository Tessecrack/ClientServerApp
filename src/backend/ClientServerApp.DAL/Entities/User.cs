using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientServerApp.DAL.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [Required]
        [JsonPropertyName("phone_number")]
        public string? PhoneNumber { get; set; } 

        public string? Email { get; set; }
    }
}