namespace SimpleService.API.Tests.Common
{
    public class ApiIntegrationTestBase: IClassFixture<TestWebApplicationFactory>
    {
        protected readonly TestWebApplicationFactory _factory;
        protected readonly HttpClient _httpClient;

        protected const string _customerEndpointUrl = "api/customers";

        public ApiIntegrationTestBase(TestWebApplicationFactory factory) {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }
    }
}
