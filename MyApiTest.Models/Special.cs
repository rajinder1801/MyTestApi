using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyApiTest.Models
{
    public class Special
    {
        [JsonProperty("quantities")]
        public IEnumerable<ProductQuantity> Quantities { get; set; }
        [JsonProperty("total")]
        public double Total { get; set; }
    }
}
