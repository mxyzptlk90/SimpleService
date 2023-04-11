using SimpleService.Core.Customer.Models;
using SimpleService.Data.InMemoryStorage;
using SimpleService.Core.Customer.Interfaces;
using SimpleService.Data.Repositories;
using Serilog;

namespace SimpleService.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/swagger/v1/swagger.json", "Customers v1"));

            app.UseRouting();

            app.UseEndpoints(app => {
                app.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services) {
            // Creating simple logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            services.AddLogging(
                    s => s.ClearProviders()
                         .AddSerilog(Log.Logger, true));

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(opts => {
                opts.SwaggerDoc("v1", new() { Title = "Customers Management", Version = "v1" });
            });

            // Registering life saving services
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly);
            });
            services.AddAutoMapper(typeof(Startup).Assembly);

            // Data emulation
            services.AddSingleton<InMemoryDataStorageWrapper<CustomerModel>>();

            services.AddScoped<ICustomerRepository, InMemoryCustomerRepository>();

        }
    }
}
