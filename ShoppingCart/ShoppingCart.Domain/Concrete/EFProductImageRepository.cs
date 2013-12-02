using ShoppingCart.Domain.Abstract;
using ShoppingCart.Domain.Entities;
using System.Linq;

namespace ShoppingCart.Domain.Concrete
{
    public class EFProductImageRepository : IProductImageRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IQueryable<ProductImage> ProductImages
        {
            get { return context.ProductImages; }
        }

        public void SaveProductImage(ProductImage productImage)
        {
            if (productImage.ProductImageID == 0)
            {
                context.ProductImages.Add(productImage);
            }
            else
            {
                ProductImage dbEntry = context.ProductImages.Find(productImage.ProductImageID);
                if (dbEntry != null)
                {
                    dbEntry.DisplayOrder = productImage.DisplayOrder;
                    dbEntry.Product = productImage.Product;
                    dbEntry.ProductImageMimeType = productImage.ProductImageMimeType;
                    dbEntry.ProductImagePath = productImage.ProductImagePath;
                }
            }
            context.SaveChanges();
        }

        public ProductImage DeleteProductImage(int productImageID)
        {
            ProductImage dbEntry = context.ProductImages.Find(productImageID);
            if (dbEntry != null)
            {
                context.ProductImages.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

    }
}
