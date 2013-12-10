using ShoppingCart.Data.Entity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ShoppingCart.Data.Concrete
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(): base("EFDbContext"){}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new CategoryNodeConfiguration());
            //modelBuilder.Configurations.Add(new ProductRelationConfiguration());

            //modelBuilder.Entity<Product>()
            //    .HasMany(p => p.RelatedProducts)
            //    .WithRequired(p=>p.Product)
            //    .HasForeignKey(p=>p.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.RelatedProducts)
                .WithRequired(p => p.RelatedProduct)
                .HasForeignKey(p => p.RelatedProductID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.RelatedProducts)
                .WithRequired(p => p.Product)
                .HasForeignKey(p => p.ProductID)
                .WillCascadeOnDelete(false);

        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
        public DbSet<ProductAttributeTranslation> ProductAttributeTranslations { get; set; }
        public DbSet<ProductAttributeValueTranslation> ProductAttributeValueTranslations { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductVariantValue> ProductVariantValues { get; set; }
        public DbSet<ProductVariantTranslation> ProductVariantTranslations { get; set; }
        public DbSet<ProductVariantValueTranslation> ProductVariantValueTranslations { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
    }

    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            HasOptional(d => d.Parent)
                .WithMany(p => p.Children)
                .Map(d => d.MapKey("ParentId"))
                .WillCascadeOnDelete(false);

            // has many ancestors
            HasMany(p => p.Ancestors)
                .WithRequired(d => d.Offspring)
                .HasForeignKey(d => d.OffspringId)
                .WillCascadeOnDelete(false);

            // has many offspring
            HasMany(p => p.Offspring)
                .WithRequired(d => d.Ancestor)
                .HasForeignKey(d => d.AncestorId)
                .WillCascadeOnDelete(false);
        }
    }

    public class CategoryNodeConfiguration : EntityTypeConfiguration<CategoryNode>
    {
        public CategoryNodeConfiguration()
        {
            ToTable(typeof(CategoryNode).Name);
            HasKey(p => new { p.AncestorId, p.OffspringId });

        }
    }

    //public class ProductRelationConfiguration : EntityTypeConfiguration<ProductRelation>
    //{
    //    public ProductRelationConfiguration()
    //    {
    //        HasKey(p => new { p.ProductID, p.RelatedProductID});
    //    }
    //}
}