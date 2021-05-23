using MyApiTest.Interfaces;
using MyApiTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApiTest.Services
{
    public class Sorter : ISorter
    {
        /// <summary>
        /// Sorts the products based on the shopper history and sort option.
        /// </summary>
        /// <param name="products">List of Products</param>
        /// <param name="shopperHistories">List of shopper history</param>
        /// <param name="sortOption">Sort Option</param>
        /// <returns>List of Sorted Products</returns>
        public IEnumerable<Product> Sort(IEnumerable<Product> products, IEnumerable<ShopperHistory> shopperHistories,
            SortOption sortOption)
        {
            return sortOption switch
            {
                SortOption.Low => products.OrderBy(x => x.Price).AsEnumerable(),
                SortOption.High => products.OrderByDescending(x => x.Price).AsEnumerable(),
                SortOption.Ascending => products.OrderBy(x => x.Name).AsEnumerable(),
                SortOption.Descending => products.OrderByDescending(x => x.Name).AsEnumerable(),
                SortOption.Recommended => RecommendedSort(products, shopperHistories),
                _ => products,
            };
        }

        private IEnumerable<Product> RecommendedSort(IEnumerable<Product> products,
            IEnumerable<ShopperHistory> shopperHistories)
        {
            var purchasedProducts = shopperHistories.SelectMany(x => x.Products).ToList();
            var sortedPurchasedProducts = purchasedProducts.GroupBy(x => x.Name)
                .Select(x => new { Name = x.Key, Count = x.Sum(y => y.Quantity) })
                .OrderByDescending(x => x.Count);
            var sortedResult = new List<Product>();
            foreach (var item in sortedPurchasedProducts)
            {
                var product = purchasedProducts.First(x => x.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
                sortedResult.Add(new Product()
                {
                    Name = product.Name,
                    Price = product.Price
                });
            }

            var nonPurchasedProducts = products.Select(x => x.Name).Except(sortedResult.Select(x => x.Name)).ToList();
            if (nonPurchasedProducts.Any())
            {
                sortedResult.AddRange(products.Where(x => nonPurchasedProducts.Contains(x.Name)));
            }
            return sortedResult;
        }
    }
}
