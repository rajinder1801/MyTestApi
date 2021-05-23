using MyApiTest.Interfaces;
using MyApiTest.Models;
using MyApiTest.Models.Config;

namespace MyApiTest.Services
{
    public class UserService : IUserService
    {
        private readonly AppConfiguration _appConfiguration;

        public UserService(AppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        /// <summary>
        /// Fetches the user
        /// </summary>
        /// <returns>User</returns>
        public User GetUser()
        {
            return _appConfiguration.User;
        }
    }
}
