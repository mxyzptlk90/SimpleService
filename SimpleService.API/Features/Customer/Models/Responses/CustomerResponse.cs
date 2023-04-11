using System.Text.Json.Serialization;

namespace SimpleService.API.Features.Customer.Models.Responses
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
    }
}
