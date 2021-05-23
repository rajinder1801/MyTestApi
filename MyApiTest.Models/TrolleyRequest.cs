using System.Collections.Generic;

namespace MyApiTest.Models
{
    //public class TrolleyRequest
    //{
    //    public IEnumerable<Product> Products { get; set; }
    //    public IEnumerable<Special> Specials { get; set; }
    //    public IEnumerable<ProductQuantity> Quantities { get; set; }
    //}

    public class TrolleyRequest
    {
        public List<Product> products { get; set; }
        public List<SpecialsGroup> specials { get; set; }
        public List<Quantity> quantities { get; set; }
    }
}
