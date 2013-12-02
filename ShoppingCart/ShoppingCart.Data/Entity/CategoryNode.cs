using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Data.Entity
{
    public class CategoryNode
    {
        public int AncestorId { get; set; }
        public virtual Category Ancestor { get; set; }
        public int OffspringId { get; set; }
        public virtual Category Offspring { get; set; }
        public int Separation { get; set; }
    }
}
