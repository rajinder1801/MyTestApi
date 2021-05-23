using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Xunit;

namespace MyApiTest.ApiClient.Tests
{
    /// <summary>
    /// Tests for HttpClientWrapper class.
    /// </summary>
    public class HttpClientWrapperTests
    {
        [Fact]
        public void HttpClientWrapper_WithDefaultRequestHeader_UsesHeaderInRequests()
        {
            var expectedRequestHeaders = new Dictionary<string, string>
            {
                {"X-Test", "TestValue"}
            };
            var serverHandler = new MockHttpMessageHandler(HttpStatusCode.OK, string.Empty);
            using (var handler = new HttpClientWrapper(
                defaultRequestHeaders: new Dictionary<string, string> {
                    { "X-Test", "TestValue"
                }
            }, handler: serverHandler))
            {

                var actualRequestHeaders = handler.Client.DefaultRequestHeaders.ToDictionary(x => x.Key,
                    x => string.Join(",", x.Value));
                actualRequestHeaders.Should().BeEquivalentTo(expectedRequestHeaders);

            }
        }


        [Fact]
        public void GenerateGetMessage_WithHeader_AddsHeadersToMessage()
        {
            var handler = new HttpClientWrapper();
            var message = handler.GenerateGetMessage(
                "http://localhost/page.htm", "test");
            message.Method.Should().BeEquivalentTo(HttpMethod.Get);
            message.RequestUri.Query.Should().BeEquivalentTo("?token=test");
        }
    }
}
