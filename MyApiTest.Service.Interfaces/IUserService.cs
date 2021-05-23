using MyApiTest.Models;

namespace MyApiTest.Interfaces
{
    /// <summary>
    /// Interface for user services.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Fetches the user
        /// </summary>
        /// <returns>User</returns>
        User GetUser();
    }
}
