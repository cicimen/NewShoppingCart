using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Entities
{
    [Table("City")]
    public class City
    {
        public int CityID { get; set; }
        [MaxLength(10)]
        public string CityCode { get; set; }
        [MaxLength(30)]
        public string CityName { get; set; }
        public int DisplayOrder { get; set; }
    }
}
