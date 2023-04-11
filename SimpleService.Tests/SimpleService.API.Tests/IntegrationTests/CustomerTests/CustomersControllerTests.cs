using Newtonsoft.Json;
using SimpleService.API.Features.Customer.Models;
using SimpleService.API.Tests.Common;
using System.Net.Http.Json;

namespace SimpleService.API.Tests.IntegrationTests.CustomerTests
{
    public class CustomersControllerTests : ApiIntegrationTestBase
    {
        public CustomersControllerTests(TestWebApplicationFactory factory) : base(factory) { }

        [Fact]
        public async Task PostCustomerTest_Created() {
            var requestBody = new { Firstname = "Test", Surname = "Test" };

            var response = await _httpClient.PostAsJsonAsync(_customerEndpointUrl, requestBody);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
            var customerResponse = JsonConvert.DeserializeObject<CustomerModelApi>(await response.Content.ReadAsStringAsync());

            Assert.True(customerResponse.Id.HasValue);
            Assert.Equal("Test", customerResponse.FirstName);
            Assert.Equal("Test", customerResponse.LastName);
            Assert.Equal($"/{_customerEndpointUrl}/{customerResponse.Id}", response.Headers.Location.ToString());
        }

        [Fact]
        public async Task GetCustomerTest_Found() {
            var id = 2;
            var response = await _httpClient.GetAsync($"{_customerEndpointUrl}/{id}");

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            var customerResponse = JsonConvert.DeserializeObject<CustomerModelApi>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(customerResponse);
            Assert.True(_factory.DataStorage.ContainsKey(id));
            Assert.Equal(_factory.DataStorage[id].FirstName, customerResponse.FirstName);
            Assert.Equal(_factory.DataStorage[id].LastName, customerResponse.LastName);
        }

        [Fact]
        public async Task GetCustomerTest_NotFound() {
            var id = int.MaxValue;
            var response = await _httpClient.GetAsync($"{_customerEndpointUrl}/{id}");

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetAllCustomersTest() {
            var response = await _httpClient.GetAsync(_customerEndpointUrl);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            var customerResponse = JsonConvert.DeserializeObject<List<CustomerModelApi>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(customerResponse);
            Assert.Equal(_factory.DataStorage.Count(), customerResponse.Count);
            customerResponse.ForEach(c => Assert.Equal(c.FirstName, _factory.DataStorage[c.Id.Value].FirstName));
        }

        [Fact]
        public async Task DeleteCustomerTest_Ok() {
            var id = 3;
            var count = _factory.DataStorage.Count();

            Assert.True(_factory.DataStorage.ContainsKey(id));

            var response = await _httpClient.DeleteAsync($"{_customerEndpointUrl}/{id}");
            var newCount = _factory.DataStorage.Count();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.False(_factory.DataStorage.ContainsKey(id));
            Assert.Equal(count - 1, newCount);
        }

        [Fact]
        public async Task DeleteCustomerTest_NotFound() {
            var id = int.MaxValue;
            var count = _factory.DataStorage.Count();
            Assert.False(_factory.DataStorage.ContainsKey(id));

            var response = await _httpClient.DeleteAsync($"{_customerEndpointUrl}/{id}");
            var newCount = _factory.DataStorage.Count();

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(count, newCount);
        }
    }
}
