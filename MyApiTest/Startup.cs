using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApiTest.ApiClient.Interfaces;
using MyApiTest.Interfaces;
using MyApiTest.Models.Config;
using MyApiTest.Services;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace MyApiTest
{
    /// <summary>
    /// Startup class that configures this web API including dependency injection setup.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.Converters.Add(new StringEnumConverter());
               })
               .AddControllersAsServices();   // Ensure Controllers are available via DI for tests

            services.AddSwaggerGen();
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<HttpMessageHandler, HttpClientHandler>();
            services.AddSingleton<IApiClient, ApiClient.ApiClient>();
            services.AddSingleton<ISortingService, SortingService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IShopperService, ShopperService>();
            services.AddSingleton<ISorter, Sorter>();
            services.AddSingleton<IShopperService, ShopperService>();
            services.AddSingleton<ITrolleyService, TrolleyService>();

            var appConfiguration = new AppConfiguration();
            _configuration.GetSection("App").Bind(appConfiguration);
            services.AddSingleton(appConfiguration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
