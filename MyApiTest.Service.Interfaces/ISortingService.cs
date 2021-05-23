using MyApiTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApiTest.Interfaces
{
    /// <summary>
    /// Interface for Sorting Service
    /// </summary>
    public interface ISortingService
    {
        /// <summary>
        /// Get the sorted products.
        /// </summary>
        /// <param name="sortOption">Sort Option</param>
        /// <returns>List of Sorted Products</returns>
        Task<IEnumerable<Product>> GetSortedProducts(SortOption sortOption);
    }
}
