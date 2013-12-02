using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ShoppingCart.Data.Entity
{
    //TODO: Bu Bind be işe yarıyor?
    //[Bind(Exclude = "OrderId")]
    [Table("Order")]
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        [Required]
        [MaxLength(200)]
        public string NameSurname { get; set; }
        [Required]
        [MaxLength(300)]
        public string AddressLine1 { get; set; }
        [MaxLength(300)]
        public string AddressLine2 { get; set; }
        [MaxLength(30)]
        public string PostalCode { get; set; }
        [Required]
        [MaxLength(30)]
        public string Phone { get; set; }
        [Required]
        [MaxLength(30)]
        public string PhoneMobile { get; set; }
        [Required]
        public string Email { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal Total { get; set; }
        public int CityID { get; set; }
        public string ApplicationUserId { get; set; }



        [ForeignKey("CityID")]
        public City City { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        private ICollection<OrderItem> _orderItems;
        public virtual ICollection<OrderItem> OrderItems
        {
            get { return _orderItems ?? (_orderItems = new Collection<OrderItem>()); }
            set { _orderItems = value; }
        }
    }
}
