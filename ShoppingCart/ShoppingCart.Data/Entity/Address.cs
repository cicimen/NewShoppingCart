using System;
using System.Text;


using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ShoppingCart.Data.Entity
{
    [Table("Address")]
    public class Address
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
        public int CityID { get; set; }
        public string ApplicationUserID { get; set; }


        [ForeignKey("CityID")]
        public City City { get; set; }

        [ForeignKey("ApplicationUserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
