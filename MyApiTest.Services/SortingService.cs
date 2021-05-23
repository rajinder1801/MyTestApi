using MyApiTest.Interfaces;
using MyApiTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApiTest.Services
{
    public class SortingService : ISortingService
    {
        private readonly IProductService _productService;
        private readonly IShopperService _shopperService;
        private readonly ISorter _sorter;

        public SortingService(IProductService productService, IShopperService shopperService, ISorter sorter)
        {
            _productService = productService;
            _shopperService = shopperService;
            _sorter = sorter;
        }

        /// <summary>
        /// Get the sorted products.
        /// </summary>
        /// <param name="sortOption">Sort Option</param>
        /// <returns>List of Sorted Products</returns>
        public async Task<IEnumerable<Product>> GetSortedProducts(SortOption sortOption)
        {
            var products = await _productService.GetProducts();
            IEnumerable<ShopperHistory> shopperHistories = null;
            if (sortOption == SortOption.Recommended)
            {
                shopperHistories = await _shopperService.GetShopperHistory();
            }

            return _sorter.Sort(products, shopperHistories, sortOption);
        }
    }
}
