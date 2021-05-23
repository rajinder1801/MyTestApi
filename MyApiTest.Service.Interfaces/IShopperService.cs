using MyApiTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApiTest.Interfaces
{
    /// <summary>
    /// Interface for Shopper Service
    /// </summary>
    public interface IShopperService
    {
        /// <summary>
        /// Get the shopper history from the shopper api.
        /// </summary>
        /// <returns>List of shopper history.</returns>
        Task<IEnumerable<ShopperHistory>> GetShopperHistory();
    }
}
