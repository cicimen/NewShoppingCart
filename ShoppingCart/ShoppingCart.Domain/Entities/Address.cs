using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShoppingCart.Domain.Concrete;
using System.ComponentModel.DataAnnotations.Schema;


namespace ShoppingCart.Domain.Entities
{
    [Table("Address")]
    public class Address
    {
        public int AddressId { get; set; }
        public string AddressDescription { get; set; }
        public string NameSurname { get; set; }
        public string AddressLine1 {get;set;}
        public string AddressLine2 {get;set;}
        public string PostalCode {get;set;}
        public string Phone { get; set; }
        public string PhoneMobile { get; set; }
        public int CityID { get; set; }
        public City City { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
