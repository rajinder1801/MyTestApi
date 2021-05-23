using MyApiTest.Interfaces;
using MyApiTest.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyApiTest.Services
{
    public class TrolleyService : ITrolleyService
    {
        /// <summary>
        /// Calculates the minimum total for the trolley request.
        /// </summary>
        /// <param name="trolleyRequest">The trolley request object.</param>
        /// <returns>Minimum trolley amount.</returns>
        public double CalculateMinimumTotal(TrolleyRequest trolleyRequest)
        {
            var tempTrolley = trolleyRequest.Quantities;
            var totalsWithSpecials = new List<TrolleyTotalsWithSpecials>();
            var trolleyTotal = default(double);
            foreach (var item in tempTrolley)
            {
                var specialOffer = trolleyRequest.Specials.SelectMany(x => x.Quantities)
                    .Where(x => x != null && x.Name == item.Name && item.Quantity >= x.Quantity).ToList();
                if (specialOffer.Any())
                {
                    ApplySpecials(trolleyRequest, totalsWithSpecials, item, specialOffer);
                }
                else
                {
                    var product = trolleyRequest.Products.FirstOrDefault(x => x.Name == item.Name);
                    totalsWithSpecials.Add(new TrolleyTotalsWithSpecials()
                    {
                        ItemName = item.Name,
                        Quantity = item.Quantity,
                        BillAmount = product != null ? product.Price * item.Quantity : default
                    });
                }
            }

            foreach (var a in totalsWithSpecials)
            {
                trolleyTotal += a.BillAmount;
            }

            return trolleyTotal;
        }

        private static void ApplySpecials(TrolleyRequest trolleyRequest, IList<TrolleyTotalsWithSpecials> totalsWithSpecials,
            ProductQuantity item, IEnumerable<ProductQuantity> specialOffer)
        {
            if (specialOffer == null || !specialOffer.Any())
            {
                return;
            }

            do
            {
                var specialOfferQty = specialOffer.Max(x => x.Quantity);
                if (specialOfferQty == default)
                {
                    continue;
                }

                int group = item.Quantity / specialOfferQty;
                int remainingQuantity = item.Quantity % specialOfferQty;
                var specialProduct = trolleyRequest.Specials.FirstOrDefault(p => p.Quantities.Any(y =>
                    y.Name == item.Name && y.Quantity == specialOfferQty));
                totalsWithSpecials.Add(new TrolleyTotalsWithSpecials()
                {
                    ItemName = item.Name,
                    Quantity = group,
                    BillAmount = specialProduct != null ? specialProduct.Total * group : default
                });
                item.Quantity = remainingQuantity;
                specialOffer = trolleyRequest.Specials.SelectMany(x => x.Quantities)
                    .Where(x => x.Name == item.Name && item.Quantity >= x.Quantity && x.Quantity != default);

            } while (specialOffer.Any() && item.Quantity >= specialOffer.Max(x => x.Quantity));

            if (item.Quantity > 0)
            {
                var product = trolleyRequest.Products.FirstOrDefault(x => x.Name == item.Name);
                totalsWithSpecials.Add(new TrolleyTotalsWithSpecials
                {
                    ItemName = item.Name,
                    Quantity = item.Quantity,
                    BillAmount = product != null ? product.Price * item.Quantity : default
                });
            }
        }
    }
}
