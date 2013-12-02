using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Data.Entity
{
    [Table("City")]
    public class City
    {
        public int CityID { get; set; }
        [MaxLength(10)]
        public string CityCode { get; set; }
        [MaxLength(50)]
        public string CityName { get; set; }
        public int DisplayOrder { get; set; }
    }
}
