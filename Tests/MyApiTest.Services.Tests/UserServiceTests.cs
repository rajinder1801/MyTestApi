using FluentAssertions;
using MyApiTest.Models;
using MyApiTest.Models.Config;
using System;
using Xunit;

namespace MyApiTest.Services.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void GetUser_ValidAppConfig_CompletesSuccessfully()
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

            var userService = new UserService(appConfig);
            var user = userService.GetUser();
            user.Should().NotBeNull().And.Equals(appConfig.User);
        }
    }
}
