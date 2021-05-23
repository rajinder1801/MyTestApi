using MyApiTest.Models;
using System.Collections.Generic;

namespace MyApiTest.Interfaces
{
    /// <summary>
    /// Interface for Sorter
    /// </summary>
    public interface ISorter
    {
        /// <summary>
        /// Sorts the products based on the shopper history and sort option.
        /// </summary>
        /// <param name="products">List of Products</param>
        /// <param name="shopperHistories">List of shopper history</param>
        /// <param name="sortOption">Sort Option</param>
        /// <returns>List of Sorted Products</returns>
        IEnumerable<Product> Sort(IEnumerable<Product> products, IEnumerable<ShopperHistory> shopperHistories,
            SortOption sortOption);
    }
}
