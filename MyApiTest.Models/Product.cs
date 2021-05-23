using Newtonsoft.Json;

namespace MyApiTest.Models
{
    public class Product
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("quantity")]
        public double Quantity { get; set; }
    }
}
