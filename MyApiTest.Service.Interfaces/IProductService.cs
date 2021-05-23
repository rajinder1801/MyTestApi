using MyApiTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApiTest.Interfaces
{
    /// <summary>
    /// Interface for Product Service
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get the Products from the product api.
        /// </summary>
        /// <returns>List of Products</returns>
        Task<IEnumerable<Product>> GetProducts();
    }
}
