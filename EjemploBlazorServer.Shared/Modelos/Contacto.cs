using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EjemploBlazorServer.Shared.Modelos
{
    public class Contacto
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        [MaxLength(20)]
        [JsonPropertyName("phonenumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [JsonPropertyName("city")]
        public string City { get; set; }
    }
}