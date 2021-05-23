using System.Collections.Generic;

namespace MyApiTest.Models
{
    //public class Special
    //{
    //    public IEnumerable<ProductQuantity> Quantities { get; set; }
    //    public int Total { get; set; }
    //}

    public class SpecialsGroup
    {
        public List<Quantity> quantities { get; set; }
        public double total { get; set; }
    }
}
