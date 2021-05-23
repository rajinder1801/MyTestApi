using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MyApiTest.ApiClient.Tests
{
    public class ApiClientTests
    {
        [Fact]
        public void GetAsync_InvalidUri_ThrowsArgumentNullException()
        {
            var httpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.OK, "testresponse");

            var apiClient = new ApiClient(httpMessageHandler);
            Func<Task> task = async () => await apiClient.GetAsync(null, null);
            task.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'uri')");
        }

        [Fact]
        public void GetAsync_InvalidToken_ThrowsArgumentNullException()
        {
            var httpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.OK, "testresponse");

            var apiClient = new ApiClient(httpMessageHandler);
            Func<Task> task = async () => await apiClient.GetAsync("http://localhost/page.htm", null);
            task.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'token')");
        }

        [Fact]
        public async Task GetAsync_WithToken_CompletesSuccessfully()
        {
            var expectedRequestHeaders = new Dictionary<string, string>
            {
                { "Accept", "application/json" }
            };
            var expectedResponse = new HttpResponseMessage
            {
                Content = new StringContent("testresponse"),
                StatusCode = HttpStatusCode.OK
            };
            var httpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.OK, "testresponse");

            var apiClient = new ApiClient(httpMessageHandler);
            var response = await apiClient.GetAsync("http://localhost/page.htm", "test");

            response.Should().BeEquivalentTo(expectedResponse);

            var actualRequestHeaders = httpMessageHandler.LastRequest.Headers.ToDictionary(x => x.Key,
                x => string.Join(",", x.Value));
            actualRequestHeaders.Should().BeEquivalentTo(expectedRequestHeaders);

            httpMessageHandler.LastRequest.RequestUri.Query.Should().BeEquivalentTo("?token=test");
        }
    }
}
