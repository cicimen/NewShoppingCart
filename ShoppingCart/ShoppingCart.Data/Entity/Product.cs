using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ShoppingCart.Data.Entity
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal OriginalPrice { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal DiscountedPrice { get; set; }

        public bool Enabled { get; set; }

        [Required]
        [MaxLength(300)]
        public string ProductURLText { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateModified { get; set; }

        [Required]
        public int Inventory { get; set; }

        [MaxLength(300)]
        public string ProductMetaTags { get; set; }

        [MaxLength(300)]
        public string ProductMetaDescription { get; set; }

        public int ProductDisplayOrder { get; set; }

        public int CategoryID { get; set; }




        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        
        public virtual ICollection<ProductImage> ProductImages
        {
            get { return _productImages ?? (_productImages = new Collection<ProductImage>()); }
            set { _productImages = value; }
        }
        
        public virtual ICollection<ProductVariant> ProductVariants
        {
            get { return _productVariants ?? (_productVariants = new Collection<ProductVariant>()); }
            set { _productVariants = value; }
        }

        public virtual ICollection<ProductAttributeValue> ProductAttributeValues
        {
            get { return _productAttributeValues ?? (_productAttributeValues = new Collection<ProductAttributeValue>()); }
            set { _productAttributeValues = value; }
        }
        
        public virtual ICollection<ProductTranslation> ProductTranslations
        {
            get { return _productTranslations ?? (_productTranslations = new Collection<ProductTranslation>()); }
            set { _productTranslations = value; }
        }
        
        public virtual ICollection<ProductComment> ProductComments
        {
            get { return _productComments ?? (_productComments = new Collection<ProductComment>()); }
            set { _productComments = value; }
        }
       
        public virtual ICollection<ProductRelation> RelatedProducts
        {
            get { return _relatedProducts ?? (_relatedProducts = new Collection<ProductRelation>()); }
            set { _relatedProducts = value; }
        }

        //public virtual ICollection<ProductRelation> RelatedToProducts
        //{
        //    get { return _relatedToProducts ?? (_relatedToProducts = new Collection<ProductRelation>()); }
        //    set { _relatedToProducts = value; }
        //}

        
        private ICollection<ProductImage> _productImages;
        private ICollection<ProductAttributeValue> _productAttributeValues;
        private ICollection<ProductVariant> _productVariants;
        private ICollection<ProductTranslation> _productTranslations;
        private ICollection<ProductComment> _productComments;
        private ICollection<ProductRelation> _relatedProducts;
        //private ICollection<ProductRelation> _relatedToProducts;


    }
}
