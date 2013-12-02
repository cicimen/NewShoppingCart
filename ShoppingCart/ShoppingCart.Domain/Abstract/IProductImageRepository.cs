using System.Linq;
using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Domain.Abstract
{
    public interface IProductImageRepository
    {
        IQueryable<ProductImage> ProductImages { get; }

        void SaveProductImage(ProductImage productImage);

        ProductImage DeleteProductImage(int productImageID);
    }
}
