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
    public class ShopperServiceTests
    {
        [Fact]
        public async Task GetShopperHistory_ValidResponse_CompletesSuccessfully()
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
                        new StringContent("[\r\n  {\r\n    \"customerId\": 123,\r\n    \"products\": [\r\n      {\r\n        " +
                                          "\"name\": \"Test Product A\",\r\n        \"price\": 99.99,\r\n        \"quantity\": 3\r\n" +
                                          "      },\r\n      {\r\n        \"name\": \"Test Product B\",\r\n        \"price\": 101.99,\r\n " +
                                          "       \"quantity\": 1\r\n      },\r\n      {\r\n        \"name\": \"Test Product F\",\r\n " +
                                          "       \"price\": 999999999999,\r\n        \"quantity\": 1\r\n      }\r\n    ]\r\n  }\r\n]")
                }));

            var shopperService = new ShopperService(appConfig, apiClient);
            var response = await shopperService.GetShopperHistory();
            response.Should().NotBeNull().And.HaveCount(1);
        }

        [Fact]
        public void GetShopperHistory_InValidResponse_ThrowsHttpRequestException()
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


            var shopperService = new ShopperService(appConfig, apiClient);
            Func<Task> task = async () => await shopperService.GetShopperHistory();
            task.Should().Throw<HttpRequestException>()
                .WithMessage("Response Code - 400 for https://test.com.au/api/resource/shopperHistory");
        }
    }
}
