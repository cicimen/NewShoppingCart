using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShoppingCart.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingCart.Service.ViewModel
{
    public class AddressViewModel
    {
        public int AddressID { get; set; }
        [Required]
        [MaxLength(75)]
        public string AddressDescription { get; set; }
        [MaxLength(200)]
        public string NameSurname { get; set; }
        [Required]
        [MaxLength(300)]
        public string AddressLine1 { get; set; }
        [MaxLength(300)]
        public string AddressLine2 { get; set; }
        [MaxLength(30)]
        public string PostalCode { get; set; }
        [MaxLength(30)]
        public string Phone { get; set; }
        [MaxLength(30)]
        public string PhoneMobile { get; set; }
        public DateTime? DateCreated { get; set; }
        [Required]
        public int CityID { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public string ApplicationUserID { get; set; }
    }
}
