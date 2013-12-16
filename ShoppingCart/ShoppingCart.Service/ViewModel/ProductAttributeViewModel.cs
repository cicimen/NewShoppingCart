using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Service.ViewModel
{
    public class ProductAttributeViewModel
    {
        public int ProductAttributeID { get; set; }
        public int ProductAttributeValueID { get; set; }
        public string ProductAttributeName { get; set; }
        public string ProductAttributeValue { get; set; }
    }
}
