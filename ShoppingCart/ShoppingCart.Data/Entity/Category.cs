using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Data.Entity
{
    [Table("Category")]
    public class Category
    {
        public int CategoryID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [MaxLength(500)]
        public string CategoryImagePath { get; set; }
        [MaxLength(200)]
        public string CategoryURLText { get; set; }
        [MaxLength(300)]
        public string CategoryMetaTags { get; set; }
        [MaxLength(300)]
        public string CategoryMetaDescription { get; set; }
        public int CategoryDisplayOrder { get; set; }
    
        
        public virtual Category Parent { get; set; }


        private ICollection<CategoryTranslation> _categoryTranslations;
        private ICollection<Category> _children;
        private ICollection<CategoryNode> _ancestors;
        private ICollection<CategoryNode> _offspring;
        public virtual ICollection<CategoryTranslation> CategoryTranslations
        {
            get { return _categoryTranslations ?? (_categoryTranslations = new Collection<CategoryTranslation>()); }
            set { _categoryTranslations = value; }
        }
        public virtual ICollection<Category> Children
        {
            get { return _children ?? (_children = new Collection<Category>()); }
            set { _children = value; }
        }
        public virtual ICollection<CategoryNode> Ancestors
        {
            get { return _ancestors ?? (_ancestors = new Collection<CategoryNode>()); }
            set { _ancestors = value; }
        }
        public virtual ICollection<CategoryNode> Offspring {
            get { return _offspring ?? (_offspring = new Collection<CategoryNode>()); }
            set { _offspring = value;  }
        }
    }
    
}
