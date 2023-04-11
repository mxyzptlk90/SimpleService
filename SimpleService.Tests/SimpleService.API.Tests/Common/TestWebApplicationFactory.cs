using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SimpleService.Data.InMemoryStorage;

namespace SimpleService.API.Tests.Common
{
    public class TestWebApplicationFactory: WebApplicationFactory<Startup>
    {
        public InMemoryDataStorageWrapper<CustomerModel> DataStorage { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder) {
            builder.ConfigureTestServices(services => {
                SeedTestDataInMemory(services);
            });
        }

        private void SeedTestDataInMemory(IServiceCollection services) {
            DataStorage = new InMemoryDataStorageWrapper<CustomerModel>();

            DataStorage.Add(new() { Id = 0, FirstName = "Abba", LastName = "Baab" });
            DataStorage.Add(new() { Id = 1, FirstName = "Hrom", LastName = "Brom" });
            DataStorage.Add(new() { Id = 2, FirstName = "Lera", LastName = "Hrov" });
            DataStorage.Add(new() { Id = 3, FirstName = "Mert", LastName = "Trem" });
            DataStorage.Add(new() { Id = 4, FirstName = "Kulo", LastName = "Baab" });
            DataStorage.Add(new() { Id = 5, FirstName = "Beta", LastName = "Bloc" });

            services.AddSingleton(DataStorage);
        }
    }
}
