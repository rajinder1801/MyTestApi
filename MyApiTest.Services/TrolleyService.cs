using MyApiTest.Interfaces;
using MyApiTest.Models;
using System;
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
            var tempTrolley = trolleyRequest.quantities;
            var totalsWithSpecials = new List<TrolleyTotalsWithSpecials>();
            var trolleyTotal = default(double);
            foreach (var item in tempTrolley)
            {
                var specialOffer = trolleyRequest.specials.SelectMany(x => x.quantities)
                    .Where(x => x !=null && x.name == item.name && item.quantity >= x.quantity).ToList();
                if (specialOffer.Any())
                {
                    ApplySpecials(trolleyRequest, totalsWithSpecials, item, specialOffer);
                }
                else
                {
                    var product = trolleyRequest.products.FirstOrDefault(x => x.name == item.name);
                    totalsWithSpecials.Add(new TrolleyTotalsWithSpecials()
                    {
                        itemName = item.name,
                        quantity = item.quantity,
                        billAmount = product != null ? product.price * item.quantity : default
                    });
                }
            }

            foreach (var a in totalsWithSpecials)
            {
                trolleyTotal += a.billAmount;
            }

            return trolleyTotal;
        }

        private static void ApplySpecials(TrolleyRequest trolleyRequest, IList<TrolleyTotalsWithSpecials> totalsWithSpecials,
            Quantity item, IEnumerable<Quantity> specialOffer)
        {
            if (specialOffer == null || !specialOffer.Any())
            {
                return;
            }

            do
            {
                var specialOfferQty = specialOffer.Max(x => x.quantity);
                if (specialOfferQty == default)
                {
                    continue;
                }

                int group = item.quantity / specialOfferQty;
                int remainingQuantity = item.quantity % specialOfferQty;
                var specialProduct = trolleyRequest.specials.FirstOrDefault(p => p.quantities.Any(y =>
                    y.name == item.name && y.quantity == specialOfferQty));
                totalsWithSpecials.Add(new TrolleyTotalsWithSpecials()
                {
                    itemName = item.name,
                    quantity = group,
                    billAmount = specialProduct !=null ? specialProduct.total * group : default
                });
                item.quantity = remainingQuantity;
                specialOffer = trolleyRequest.specials.SelectMany(x => x.quantities)
                    .Where(x => x.name == item.name && item.quantity >= x.quantity && x.quantity != default);

            } while (specialOffer.Any() && item.quantity >= specialOffer.Max(x => x.quantity));

            if (item.quantity > 0)
            {
                var product = trolleyRequest.products.FirstOrDefault(x => x.name == item.name);
                totalsWithSpecials.Add(new TrolleyTotalsWithSpecials
                {
                    itemName = item.name,
                    quantity = item.quantity,
                    billAmount = product !=null ? product.price * item.quantity : default
                });
            }
        }
    }
}
