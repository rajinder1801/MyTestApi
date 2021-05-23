using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MyApiTest.ApiClient.Tests
{
    /// <summary>
    /// Mock Http Message Handler model
    /// </summary>
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _responseContent;
        private readonly HttpStatusCode _statusCode;

        /// <summary>
        /// The number of invocations of the Http method.
        /// </summary>
        public int NumberOfCalls { get; private set; }

        /// <summary>
        /// The last request for the Http call.
        /// </summary>
        public HttpRequestMessage LastRequest { get; private set; }

        public MockHttpMessageHandler(HttpStatusCode statusCode, string responseContent)
        {
            _statusCode = statusCode;
            _responseContent = responseContent;
        }

        /// <summary>
        /// Mocks the Send operation for an HttpRequestMessage
        /// </summary>
        /// <param name="request">Http Message Request</param>
        /// <returns>Http Message Response</returns>
        public virtual HttpResponseMessage Send(HttpRequestMessage request)
        {
            NumberOfCalls++;
            LastRequest = request;

            return new HttpResponseMessage
            {
                StatusCode = _statusCode,
                Content = new StringContent(_responseContent)
            };
        }


        /// <summary>
        /// Mocks the Send operation for an HttpRequestMessage
        /// </summary>
        /// <param name="request">Http Message Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Http Message Response</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(Send(request));
        }
    }
}
