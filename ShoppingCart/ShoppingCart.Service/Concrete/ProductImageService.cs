using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Service
{
    public class ProductImageService : IProductImageService
    {
        private readonly IContext _context;
        private readonly IProductImageRepository _productImages;

        public ProductImageService(IContext context)
        {
            _context = context;
            _productImages = context.ProductImages;
        }

        public ProductImage GetBy(int id)
        {
            return _productImages.Find(pi=> pi.ProductImageID == id);
        }
    }
}
