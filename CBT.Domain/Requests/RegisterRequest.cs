using Swashbuckle.AspNetCore.Annotations;

namespace CBT.Domain.Requests
{
    public class RegisterRequest
    {
        [SwaggerSchema("Username")]
        public required string Name { get; set; }
        [SwaggerSchema("Email")]
        public required string Email { get; set; }
        [SwaggerSchema("Password")]
        public required string Password { get; set; }
        [SwaggerSchema("Nickname(optional)")]
        public string? Username { get; set; } = null;

    }
}
