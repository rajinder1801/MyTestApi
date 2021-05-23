using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MyApiTest.ApiClient
{
    /// <summary>
    /// Http Client wrapper
    /// </summary>
    public class HttpClientWrapper : IDisposable
    {
        private volatile bool _disposed;
        private readonly HttpClient _client;

        /// <summary>
        /// Creates an instance of the <see cref="HttpClientWrapper"/>.
        /// </summary>
        public HttpClientWrapper(IDictionary<string, string> defaultRequestHeaders = null,
            HttpMessageHandler handler = null)
        {
            _client = handler != null
                ? new HttpClient(handler)
                : new HttpClient(
                    new HttpClientHandler
                    {
                        MaxConnectionsPerServer = 100
                    });

            AddDefaultHeaders(defaultRequestHeaders);
        }

        /// <summary>
        /// Gets the headers which should be sent with each request.
        /// </summary>
        public IDictionary<string, string> DefaultRequestHeaders
            => _client.DefaultRequestHeaders.ToDictionary(x => x.Key, x => x.Value.First());

        /// <summary>
        /// Sends the given <paramref name="request"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
            => SendAsync(request, HttpCompletionOption.ResponseContentRead,
                CancellationToken.None);

        /// <summary>
        /// Sends the given <paramref name="request"/> with the given <paramref name="option"/> 
        /// and <paramref name="token"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            HttpCompletionOption option, CancellationToken token)
        {
            return _client.SendAsync(request, option, token);
        }

        /// <summary>
        /// Generate a <c>GET</c> HttpRequestMessage
        /// </summary>
        /// <param name="uri">Uri for the request</param>
        /// <param name="token">Token</param>
        /// <returns>Populated HttpRequestMessage for the <c>GET</c> request</returns>
        public HttpRequestMessage GenerateGetMessage(string uri, string token)
        {
            var uriBuilder = new UriBuilder(uri);
            uriBuilder.Query = $"token={token}";

            var message = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
            return message;
        }

        /// <summary>
        /// Cancels all pending requests on this instance.
        /// </summary>
        public void CancelPendingRequests() => _client.CancelPendingRequests();

        /// <summary>
        /// Releases the unmanaged resources and disposes of the managed resources used by the <see cref="HttpClientWrapper"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources and disposes of the managed resources used by the <see cref="HttpClientWrapper"/>.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                CancelPendingRequests();
                _client?.Dispose();
                _disposed = true;
            }
        }

        private void AddDefaultHeaders(IDictionary<string, string> headers)
        {
            if (headers == null)
            { return; }
            foreach (var item in headers)
            {
                _client.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);
            }
        }
    }
}
