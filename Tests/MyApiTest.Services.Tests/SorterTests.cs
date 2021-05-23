using FluentAssertions;
using MyApiTest.Models;
using System.Collections.Generic;
using Xunit;

namespace MyApiTest.Services.Tests
{
    public class SorterTests
    {
        [Fact]
        public void Sort_RecommendedSortOption_ReturnsCorrectData()
        {
            var expected = new List<Product>
            {
                new Product
                {
                    Name = "2",
                    Price = 1
                },
                new Product
                {
                    Name = "1",
                    Price = 10
                },
                new Product
                {
                    Name = "3",
                    Price = 100
                }
            };

            var products = new List<Product>
            {
                new Product
                {
                    Name = "3",
                    Price = 100
                },
                new Product
                {
                    Name = "1",
                    Price = 10
                },
                new Product
                {
                    Name = "2",
                    Price = 1
                }
            };
            var shopperHistories = new List<ShopperHistory>
            {
                new ShopperHistory
                {
                    CustomerId = 1,
                    Products = new List<Product>
                    {
                        new Product
                        {
                            Name = "1",
                            Price = 10,
                            Quantity = 5
                        },
                        new Product
                        {
                            Name = "2",
                            Price = 1,
                            Quantity = 6
                        }
                    }
                }
            };

            var sorter = new Sorter();
            var actual = sorter.Sort(products, shopperHistories, SortOption.Recommended);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Sort_LowSortOption_ReturnsCorrectData()
        {
            var expected = new List<Product>
            {
                new Product
                {
                    Name = "2",
                    Price = 1
                },
                new Product
                {
                    Name = "1",
                    Price = 10
                },
                new Product
                {
                    Name = "3",
                    Price = 100
                }
            };

            var products = new List<Product>
            {
                new Product
                {
                    Name = "3",
                    Price = 100
                },
                new Product
                {
                    Name = "1",
                    Price = 10
                },
                new Product
                {
                    Name = "2",
                    Price = 1
                }
            };

            var sorter = new Sorter();
            var actual = sorter.Sort(products, null, SortOption.Low);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Sort_HighSortOption_ReturnsCorrectData()
        {
            var expected = new List<Product>
            {
                new Product
                {
                    Name = "3",
                    Price = 100
                },
                new Product
                {
                    Name = "1",
                    Price = 10
                },
                new Product
                {
                    Name = "2",
                    Price = 1
                }
            };

            var products = new List<Product>
            {
                new Product
                {
                    Name = "3",
                    Price = 100
                },
                new Product
                {
                    Name = "1",
                    Price = 10
                },
                new Product
                {
                    Name = "2",
                    Price = 1
                }
            };

            var sorter = new Sorter();
            var actual = sorter.Sort(products, null, SortOption.High);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Sort_AscendingSortOption_ReturnsCorrectData()
        {
            var expected = new List<Product>
            {
                new Product
                {
                    Name = "1",
                    Price = 10
                },
                new Product
                {
                    Name = "2",
                    Price = 1
                },
                new Product
                {
                    Name = "3",
                    Price = 100
                }
            };

            var products = new List<Product>
            {
                new Product
                {
                    Name = "3",
                    Price = 100
                },
                new Product
                {
                    Name = "1",
                    Price = 10
                },
                new Product
                {
                    Name = "2",
                    Price = 1
                }
            };

            var sorter = new Sorter();
            var actual = sorter.Sort(products, null, SortOption.Ascending);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Sort_DescendingSortOption_ReturnsCorrectData()
        {
            var expected = new List<Product>
            {
                new Product
                {
                    Name = "3",
                    Price = 100
                },
                new Product
                {
                    Name = "2",
                    Price = 1
                },
                new Product
                {
                    Name = "1",
                    Price = 10
                }
            };

            var products = new List<Product>
            {
                new Product
                {
                    Name = "3",
                    Price = 100
                },
                new Product
                {
                    Name = "1",
                    Price = 10
                },
                new Product
                {
                    Name = "2",
                    Price = 1
                }
            };

            var sorter = new Sorter();
            var actual = sorter.Sort(products, null, SortOption.Descending);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
