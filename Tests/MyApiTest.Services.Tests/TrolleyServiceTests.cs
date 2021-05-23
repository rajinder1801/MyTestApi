using FluentAssertions;
using MyApiTest.Models;
using System.Collections.Generic;
using Xunit;

namespace MyApiTest.Services.Tests
{
    public class TrolleyServiceTests
    {
        [Fact]
        public void CalculateMinimumTotal_ValidInput_ReturnsCorrectAmount()
        {
            var trolleyRequest = new TrolleyRequest
            {
                Products = new List<Product>
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
                },
                Quantities = new List<ProductQuantity>
                {
                    new ProductQuantity
                    {
                        Name = "1",
                        Quantity = 10
                    },
                    new ProductQuantity
                    {
                        Name = "2",
                        Quantity = 5
                    },
                    new ProductQuantity
                    {
                        Name = "3",
                        Quantity = 1
                    }

                },
                Specials = new List<Special>
                {
                    new Special
                    {
                        Total = 1,
                        Quantities = new List<ProductQuantity>
                        {
                            new ProductQuantity
                            {
                                Name = "1",
                                Quantity = 1
                            }
                        }
                    },
                    new Special
                    {
                        Total = 5,
                        Quantities = new List<ProductQuantity>
                        {
                            new ProductQuantity
                            {
                                Name = "2",
                                Quantity = 5
                            }
                        }
                    }
                }
            };

            var trolleyService = new TrolleyService();
            var actualAmount = trolleyService.CalculateMinimumTotal(trolleyRequest);
            actualAmount.Should().Be(115);
        }
    }
}
