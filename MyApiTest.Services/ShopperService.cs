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
    public class ShopperService : IShopperService
    {
        private readonly AppConfiguration _appConfiguration;
        private readonly IApiClient _apiClient;

        static ShopperService()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public ShopperService(AppConfiguration appConfiguration, IApiClient apiClient)
        {
            _appConfiguration = appConfiguration;
            _apiClient = apiClient;
        }

        /// <summary>
        /// Get the shopper history from the shopper api.
        /// </summary>
        /// <returns>List of shopper history.</returns>
        public async Task<IEnumerable<ShopperHistory>> GetShopperHistory()
        {
            var requestUrl = $"{_appConfiguration.WooliesApiUrl}api/resource/shopperHistory";
            var response = await _apiClient.GetAsync(requestUrl, _appConfiguration.User.Token.ToString());
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var shopperHistory = JsonConvert.DeserializeObject<IEnumerable<ShopperHistory>>(responseContent);
                return shopperHistory;
            }

            throw new HttpRequestException($"Response Code - {(int)response.StatusCode} for {requestUrl}");
        }
    }
}
