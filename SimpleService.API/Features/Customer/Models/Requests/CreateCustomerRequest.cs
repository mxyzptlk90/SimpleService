using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SimpleService.API.Features.Customer.Models
{
    public class CreateCustomerRequest
    {
        [JsonRequired]
        [StringLength(100)]
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }
        [JsonRequired]
        [StringLength(100)]
        [JsonPropertyName("surname")]
        public string Surname { get; set; }
    }
}
