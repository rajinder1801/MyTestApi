using MyApiTest.ApiClient.Interfaces;
using MyApiTest.Interfaces;
using MyApiTest.Models;
using MyApiTest.Models.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyApiTest.Services
{
    public class ProductService : IProductService
    {
        private readonly AppConfiguration _appConfiguration;
        private readonly IApiClient _apiClient;

        static ProductService()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public ProductService(AppConfiguration appConfiguration, IApiClient apiClient)
        {
            _appConfiguration = appConfiguration;
            _apiClient = apiClient;
        }

        /// <summary>
        /// Get the Products from the product api.
        /// </summary>
        /// <returns>List of Products</returns>
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var requestUrl = $"{_appConfiguration.WooliesApiUrl}api/resource/products";
            var response = await _apiClient.GetAsync(requestUrl, _appConfiguration.User.Token.ToString());
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(responseContent);
                return products;
            }

            throw new HttpRequestException($"Response Code - {(int)response.StatusCode} for {requestUrl}");
        }
    }
}
