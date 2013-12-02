using ShoppingCart.Data.Entity;
using System.Collections.Generic;

namespace ShoppingCart.Service
{
    public interface IProductImageService
    {
        ProductImage GetBy(int id);
    }
}
