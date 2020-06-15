using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EjemploBlazorServer.Shared.Modelos
{
    public class Usuario
    {
        [JsonPropertyName("userName")]
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [JsonPropertyName("access_token")]
        public string AccesToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int Expiration { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }
}