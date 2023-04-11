using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace SimpleService.API.Features.Customer.Models
{
    public class CustomerModelApi
    {
        public int? Id { get; set; }
        [JsonProperty("firstname")]
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }
        [JsonProperty("surname")]
        [JsonPropertyName("surname")]
        public string LastName { get; set; }
    }
}
