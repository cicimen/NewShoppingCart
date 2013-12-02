using ShoppingCart.Domain.Abstract;
using ShoppingCart.Domain.Entities;
using System.Linq;

namespace ShoppingCart.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {                   
                    dbEntry.Category = product.Category;
                    dbEntry.DateCreated = product.DateCreated;
                    dbEntry.DateModified = product.DateModified;
                    dbEntry.DiscountedPrice = product.DiscountedPrice;
                    dbEntry.Enabled = product.Enabled;
                    dbEntry.Inventory = product.Inventory;
                    dbEntry.OriginalPrice = product.OriginalPrice;
                    dbEntry.ProductAttributes = product.ProductAttributes;
                    dbEntry.ProductImages = product.ProductImages;
                    dbEntry.ProductMetaDescription = product.ProductMetaDescription;
                    dbEntry.ProductMetaTags = product.ProductMetaTags;
                    dbEntry.ProductTranslations = product.ProductTranslations;
                    dbEntry.ProductURLText = product.ProductURLText;
                    dbEntry.RelatedProducts = product.RelatedProducts;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products.Find(productID);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
