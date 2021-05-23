using MyApiTest.ApiClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyApiTest.ApiClient
{
    /// <summary>
    /// Api Client
    /// </summary>
    public class ApiClient : IApiClient
    {
        private const string JsonMediaType = "application/json";

        private readonly HttpClientWrapper _client;

        public ApiClient(HttpMessageHandler httpMessageHandler)
        {
            var headers = new Dictionary<string, string>
            {
                { "Accept", JsonMediaType }
            };
            _client = new HttpClientWrapper(
                defaultRequestHeaders: headers,
                handler: httpMessageHandler);
        }

        public async Task<HttpResponseMessage> GetAsync(string uri, string token)
        {
            ValidateString(nameof(uri), uri);
            ValidateString(nameof(token), token);

            var message = _client.GenerateGetMessage(uri, token);
            return await _client.SendAsync(message);
        }

        private static void ValidateString(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
