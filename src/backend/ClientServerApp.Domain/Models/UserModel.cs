using System.Text.Json.Serialization;

namespace ClientServerApp.Domain
{
    public class UserModel
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [JsonPropertyName("phone_number")]
        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
    }
}