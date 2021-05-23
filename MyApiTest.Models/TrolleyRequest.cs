using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyApiTest.Models
{
    public class TrolleyRequest
    {
        [JsonProperty("products")]
        public IEnumerable<Product> Products { get; set; }

        [JsonProperty("specials")]
        public IEnumerable<Special> Specials { get; set; }

        [JsonProperty("quantities")]
        public IEnumerable<ProductQuantity> Quantities { get; set; }
    }
}
