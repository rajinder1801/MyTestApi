using Newtonsoft.Json;

namespace MyApiTest.Models
{
    public class ProductQuantity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
