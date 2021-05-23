using FluentAssertions;
using MyApiTest.Interfaces;
using MyApiTest.Models;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyApiTest.Services.Tests
{
    public class SortingServiceTests
    {
        [Fact]
        public async Task GetSortedProducts_RecommendedSortOption_CompletesSuccessfully()
        {
            var expectedProducts = new List<Product> { new Product { Name = "test1", Price = 10, Quantity = 10 } };
            var productService = NSubstitute.Substitute.For<IProductService>();
            var shopperService = NSubstitute.Substitute.For<IShopperService>();
            var sorter = Substitute.For<ISorter>();
            sorter.Sort(Arg.Any<IEnumerable<Product>>(), Arg.Any<IEnumerable<ShopperHistory>>(), SortOption.Recommended)
                .Returns(expectedProducts);

            var sortingService = new SortingService(productService, shopperService, sorter);
            var actualProducts = await sortingService.GetSortedProducts(SortOption.Recommended);
            actualProducts.Should().BeEquivalentTo(expectedProducts);
            await productService.Received(1).GetProducts();
            await shopperService.Received(1).GetShopperHistory();
        }

        [Fact]
        public async Task GetSortedProducts_LowSortOption_CompletesSuccessfully()
        {
            var expectedProducts = new List<Product> { new Product { Name = "test1", Price = 10, Quantity = 10 } };
            var productService = NSubstitute.Substitute.For<IProductService>();
            var shopperService = NSubstitute.Substitute.For<IShopperService>();
            var sorter = Substitute.For<ISorter>();
            sorter.Sort(Arg.Any<IEnumerable<Product>>(), Arg.Any<IEnumerable<ShopperHistory>>(), SortOption.Low)
                .Returns(expectedProducts);

            var sortingService = new SortingService(productService, shopperService, sorter);
            var actualProducts = await sortingService.GetSortedProducts(SortOption.Low);
            actualProducts.Should().BeEquivalentTo(expectedProducts);
            await productService.Received(1).GetProducts();
            await shopperService.Received(0).GetShopperHistory();
        }
    }
}
