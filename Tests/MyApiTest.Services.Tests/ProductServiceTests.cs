using FluentAssertions;
using MyApiTest.ApiClient.Interfaces;
using MyApiTest.Models;
using MyApiTest.Models.Config;
using NSubstitute;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MyApiTest.Services.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetProducts_ValidResponse_CompletesSuccessfully()
        {
            var appConfig = new AppConfiguration
            {
                WooliesApiUrl = "https://test.com.au",
                User = new User
                {
                    Name = "test",
                    Token = Guid.NewGuid()
                }
            };
            var apiClient = NSubstitute.Substitute.For<IApiClient>();
            apiClient.GetAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(
                Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content =
                        new StringContent("[\r\n  {\r\n    \"name\": \"Test Product A\",\r\n    \"price\": 99.99,\r\n    " +
                                          "\"quantity\": 0\r\n  },\r\n  {\r\n    \"name\": \"Test Product B\",\r\n    \"price\": 101.99,\r\n    " +
                                          "\"quantity\": 0\r\n  },\r\n  {\r\n    \"name\": \"Test Product C\",\r\n    \"price\": 10.99,\r\n    " +
                                          "\"quantity\": 0\r\n  },\r\n  {\r\n    \"name\": \"Test Product D\",\r\n    \"price\": 5,\r\n    " +
                                          "\"quantity\": 0\r\n  },\r\n  {\r\n    \"name\": \"Test Product F\",\r\n    \"price\": 999999999999,\r\n    " +
                                          "\"quantity\": 0\r\n  }\r\n]")
                }));

            var productService = new ProductService(appConfig, apiClient);
            var products = await productService.GetProducts();
            products.Should().NotBeNull().And.HaveCount(5);
        }

        [Fact]
        public void GetProducts_InValidResponse_ThrowsHttpRequestException()
        {
            var appConfig = new AppConfiguration
            {
                WooliesApiUrl = "https://test.com.au/",
                User = new User
                {
                    Name = "test",
                    Token = Guid.NewGuid()
                }
            };
            var apiClient = NSubstitute.Substitute.For<IApiClient>();
            apiClient.GetAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(
                Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                }));

            var productService = new ProductService(appConfig, apiClient);
            Func<Task> task = async () => await productService.GetProducts();
            task.Should().Throw<HttpRequestException>()
                .WithMessage("Response Code - 400 for https://test.com.au/api/resource/products");
        }
    }
}
