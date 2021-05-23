using System.Net.Http;
using System.Threading.Tasks;

namespace MyApiTest.ApiClient.Interfaces
{
    public interface IApiClient
    {
        /// <summary>
        /// Send a GET request to the specified Uri with a optional bearer token
        /// </summary>
        /// <param name="uri"> The Uri the request is sent to.</param>
        /// <param name="token">The authorization token(optional).</param>
        /// <returns>HttpResponseMessage object</returns>
        Task<HttpResponseMessage> GetAsync(string uri, string token);
    }
}
