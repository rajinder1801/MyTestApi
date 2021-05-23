using System;
using System.Collections.Generic;
using System.Text;

namespace MyApiTest.ApiClient.Tests
{
    /// <summary>
    /// Tests for HttpClientWrapper class.
    /// </summary>
    public class HttpClientWrapperTests
    {
        [Fact]
        public void HttpClientWrapper_WithBaseAddress_UsesBaseAddressInRequests()
        {
            var serverHandler = new MockHttpMessageHandler(HttpStatusCode.OK, string.Empty);
            var handler = new HttpClientWrapper(new Uri("http://localhost:1234/test/"),
                handler: serverHandler);
            Func<Task> task = async () => await handler.GetAsync("subdir/page.htm");
            task.Should().NotThrow();
            serverHandler.LastRequest.RequestUri.ToString().Should()
                .BeEquivalentTo("http://localhost:1234/test/subdir/page.htm");
        }

        [Fact]
        public void HttpClientWrapper_WithDefaultRequestHeader_UsesHeaderInRequests()
        {
            var expectedRequestHeaders = new Dictionary<string, string>
            {
                { "X-Test", "TestValue" }
            };
            var serverHandler = new MockHttpMessageHandler(HttpStatusCode.OK, string.Empty);
            var handler = new HttpClientWrapper(defaultRequestHeaders: new Dictionary<string, string>
                {
                    { "X-Test", "TestValue" }
                }, handler: serverHandler);
            Func<Task> task = async () => await handler.GetAsync("http://localhost/page.htm");

            task.Should().NotThrow();
            var actualRequestHeaders = serverHandler.LastRequest.Headers.ToDictionary(x => x.Key,
                x => string.Join(",", x.Value));
            actualRequestHeaders.Should().BeEquivalentTo(expectedRequestHeaders);
        }

        [Fact]
        public void GenerateGetMessage_WithHeader_AddsHeadersToMessage()
        {
            var expectedRequestHeaders = new Dictionary<string, string>
            {
                { "X-Test", "TestValue" }
            };
            var handler = new HttpClientWrapper();
            var message = handler.GenerateGetMessage(
                "http://localhost/page.htm",
                new Dictionary<string, string>
                {
                    { "X-Test", "TestValue" }
                });
            message.Method.Should().BeEquivalentTo(HttpMethod.Get);
            var actualRequestHeaders = message.Headers.ToDictionary(x => x.Key,
                x => string.Join(",", x.Value));
            actualRequestHeaders.Should().BeEquivalentTo(expectedRequestHeaders);
        }

        [Fact]
        public void GetAsync_WithHeader_AddsHeadersToMessage()
        {
            var expectedRequestHeaders = new Dictionary<string, string>
            {
                { "X-Test", "TestValue" }
            };
            var serverHandler = new MockHttpMessageHandler(HttpStatusCode.OK, string.Empty);
            var handler = new HttpClientWrapper(handler: serverHandler);
            Func<Task> task = async () => await handler.GetAsync(
                "http://localhost/page.htm",
                new Dictionary<string, string>
                {
                    { "X-Test", "TestValue" }
                });

            task.Should().NotThrow();
            var message = serverHandler.LastRequest;
            message.Method.Should().BeEquivalentTo(HttpMethod.Get);
            var actualRequestHeaders = message.Headers.ToDictionary(x => x.Key,
                x => string.Join(",", x.Value));
            actualRequestHeaders.Should().BeEquivalentTo(expectedRequestHeaders);
        }

        [Fact]
        public void GeneratePutMessage_WithHeader_AddsHeadersToMessage()
        {
            var expectedRequestHeaders = new Dictionary<string, string>
            {
                { "X-Test", "TestValue" }
            };
            var handler = new HttpClientWrapper();
            var content = new StringContent("Test Body", Encoding.UTF8, "text/html");
            var message = handler.GeneratePutMessage(
                "http://localhost/page.htm",
                content,
                new Dictionary<string, string>
                {
                    { "X-Test", "TestValue" }
                });

            message.Method.Should().BeEquivalentTo(HttpMethod.Put);
            message.Content.Should().BeEquivalentTo(content);
            var actualRequestHeaders = message.Headers.ToDictionary(x => x.Key,
                x => string.Join(",", x.Value));
            actualRequestHeaders.Should().BeEquivalentTo(expectedRequestHeaders);
        }

        [Fact]
        public void GeneratePutMessage_WithObject_FormatsObjectIntoMessageBody()
        {
            var handler = new HttpClientWrapper();
            var formatter = Substitute.For<IContentFormatter>();
            var content = new StringContent("Test Output", Encoding.UTF8, "text/html");
            formatter.FormatContent("Test Input", Encoding.UTF8).Returns(content);

            var message = handler.GeneratePutMessage(
                "http://localhost/page.htm",
                "Test Input",
                formatter);
            message.Method.Should().BeEquivalentTo(HttpMethod.Put);
            message.Content.Should().BeEquivalentTo(content);
        }

        [Fact]
        public void PutAsync_WithHeader_AddsHeadersToMessage()
        {
            var expectedRequestHeaders = new Dictionary<string, string>
            {
                { "X-Test", "TestValue" }
            };
            var serverHandler = new MockHttpMessageHandler(HttpStatusCode.OK, string.Empty);
            var handler = new HttpClientWrapper(handler: serverHandler);
            var content = new StringContent("Test Body", Encoding.UTF8, "text/html");
            Func<Task> task = async () => await handler.PutAsync(
                "http://localhost/page.htm",
                content,
                new Dictionary<string, string>
                {
                    { "X-Test", "TestValue" }
                });

            task.Should().NotThrow();
            var message = serverHandler.LastRequest;
            message.Method.Should().BeEquivalentTo(HttpMethod.Put);
            message.Content.Should().BeEquivalentTo(content);
            var actualRequestHeaders = message.Headers.ToDictionary(x => x.Key,
                x => string.Join(",", x.Value));
            actualRequestHeaders.Should().BeEquivalentTo(expectedRequestHeaders);
        }

        [Fact]
        public void PutAsync_WithObject_FormatsObjectIntoMessageBody()
        {
            var serverHandler = new MockHttpMessageHandler(HttpStatusCode.OK, string.Empty);
            var handler = new HttpClientWrapper(handler: serverHandler);
            var formatter = Substitute.For<IContentFormatter>();
            var content = new StringContent("Test Output", Encoding.UTF8, "text/html");
            formatter
                .FormatContent("Test Input", Encoding.UTF8)
                .Returns(content);
            Func<Task> task = async () => await handler.PutAsync(
                "http://localhost/page.htm",
                "Test Input",
                formatter);

            task.Should().NotThrow();
            var message = serverHandler.LastRequest;
            message.Method.Should().BeEquivalentTo(HttpMethod.Put);
            message.Content.Should().BeEquivalentTo(content);
        }

        [Fact]
        public void GeneratePostMessage_WithHeader_AddsHeadersToMessage()
        {
            var expectedRequestHeaders = new Dictionary<string, string>
            {
                { "X-Test", "TestValue" }
            };
            var handler = new HttpClientWrapper();
            var content = new StringContent("Test Body", Encoding.UTF8, "text/html");
            var message = handler.GeneratePostMessage(
                "http://localhost/page.htm",
                content,
                new Dictionary<string, string>
                {
                    { "X-Test", "TestValue" }
                });

            message.Method.Should().BeEquivalentTo(HttpMethod.Post);
            message.Content.Should().BeEquivalentTo(content);
            var actualRequestHeaders = message.Headers.ToDictionary(x => x.Key,
                x => string.Join(",", x.Value));
            actualRequestHeaders.Should().BeEquivalentTo(expectedRequestHeaders);
        }

        [Fact]
        public void GeneratePostMessage_WithObject_FormatsObjectIntoMessageBody()
        {
            var handler = new HttpClientWrapper();
            var formatter = Substitute.For<IContentFormatter>();
            var content = new StringContent("Test Output", Encoding.UTF8, "text/html");
            formatter
                .FormatContent("Test Input", Encoding.UTF8)
                .Returns(content);
            var message = handler.GeneratePostMessage(
                "http://localhost/page.htm",
                "Test Input",
                formatter);
            message.Method.Should().BeEquivalentTo(HttpMethod.Post);
            message.Content.Should().BeEquivalentTo(content);
        }

        [Fact]
        public void PostAsync_WithHeader_AddsHeadersToMessage()
        {
            var expectedRequestHeaders = new Dictionary<string, string>
            {
                { "X-Test", "TestValue" }
            };
            var serverHandler = new MockHttpMessageHandler(HttpStatusCode.OK, string.Empty);
            var handler = new HttpClientWrapper(handler: serverHandler);
            var content = new StringContent("Test Body", Encoding.UTF8, "text/html");
            Func<Task> task = async () => await handler.PostAsync(
                "http://localhost/page.htm",
                content,
                new Dictionary<string, string>
                {
                    { "X-Test", "TestValue" }
                });

            task.Should().NotThrow();
            var message = serverHandler.LastRequest;
            message.Method.Should().BeEquivalentTo(HttpMethod.Post);
            message.Content.Should().BeEquivalentTo(content);
            var actualRequestHeaders = message.Headers.ToDictionary(x => x.Key,
                x => string.Join(",", x.Value));
            actualRequestHeaders.Should().BeEquivalentTo(expectedRequestHeaders);
        }

        [Fact]
        public void PostAsync_WithObject_FormatsObjectIntoMessageBody()
        {
            var serverHandler = new MockHttpMessageHandler(HttpStatusCode.OK, string.Empty);
            var handler = new HttpClientWrapper(handler: serverHandler);
            var formatter = Substitute.For<IContentFormatter>();
            var content = new StringContent("Test Output", Encoding.UTF8, "text/html");
            formatter
                .FormatContent("Test Input", Encoding.UTF8)
                .Returns(content);
            Func<Task> task = async () => await handler.PostAsync(
                "http://localhost/page.htm",
                "Test Input",
                formatter);

            task.Should().NotThrow();
            var message = serverHandler.LastRequest;
            message.Method.Should().BeEquivalentTo(HttpMethod.Post);
            message.Content.Should().BeEquivalentTo(content);
        }

        [Fact]
        public void GenerateDeleteMessage_WithHeader_AddsHeadersToMessage()
        {
            var expectedRequestHeaders = new Dictionary<string, string>
            {
                { "X-Test", "TestValue" }
            };
            var handler = new HttpClientWrapper();
            var message = handler.GenerateDeleteMessage(
                "http://localhost/page.htm",
                new Dictionary<string, string>
                {
                    { "X-Test", "TestValue" }
                });
            Assert.Equal(HttpMethod.Delete, message.Method);
            message.Method.Should().BeEquivalentTo(HttpMethod.Delete);
            var actualRequestHeaders = message.Headers.ToDictionary(x => x.Key,
                x => string.Join(",", x.Value));
            actualRequestHeaders.Should().BeEquivalentTo(expectedRequestHeaders);
        }

        [Fact]
        public void DeleteAsync_WithHeader_AddsHeadersToMessage()
        {
            var expectedRequestHeaders = new Dictionary<string, string>
            {
                { "X-Test", "TestValue" }
            };
            var serverHandler = new MockHttpMessageHandler(HttpStatusCode.OK, string.Empty);
            var handler = new HttpClientWrapper(handler: serverHandler);
            Func<Task> task = async () => await handler.DeleteAsync("http://localhost/page.htm",
                new Dictionary<string, string> {
                    { "X-Test", "TestValue" }
                });

            task.Should().NotThrow();
            var message = serverHandler.LastRequest;
            message.Method.Should().BeEquivalentTo(HttpMethod.Delete);
            var actualRequestHeaders = message.Headers.ToDictionary(x => x.Key,
                x => string.Join(",", x.Value));
            actualRequestHeaders.Should().BeEquivalentTo(expectedRequestHeaders);
        }
    }
}
