using System.Text.Json.Serialization;

namespace API.Core.Models
{
    public class TenantProfile
    {
        [JsonPropertyName("tenantId")]
        public string TenantId { get; set; } = string.Empty;

        [JsonPropertyName("tenantName")]
        public string TenantName { get; set; } = string.Empty;

        [JsonPropertyName("baseUrl")]
        public string BaseUrl { get; set; } = string.Empty;

        [JsonPropertyName("users")]
        public List<UserProfile> Users { get; set; } = new();

        [JsonPropertyName("config")]
        public TenantConfig Config { get; set; } = new();
    }

    public class UserProfile
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; } = string.Empty;

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("defaultTimekeeperIndex")]
        public string DefaultTimekeeperIndex { get; set; } = string.Empty;

        [JsonPropertyName("defaultTimekeeperNumber")]
        public string DefaultTimekeeperNumber { get; set; } = string.Empty;
    }

    public class TenantConfig
    {
        [JsonPropertyName("clientId")]
        public string ClientId { get; set; } = string.Empty;

        [JsonPropertyName("authority")]
        public string Authority { get; set; } = string.Empty;

        [JsonPropertyName("scope")]
        public string[] Scope { get; set; } = Array.Empty<string>();

        [JsonPropertyName("redirectUri")]
        public string RedirectUri { get; set; } = string.Empty;
    }
}