using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ShoppingCart.Data.Entity
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DateCreated { get; set; }


        private ICollection<Address> _addresses;
        public virtual ICollection<Address> Addresses
        {
            get { return _addresses ?? (_addresses = new Collection<Address>()); }
            set { _addresses = value;  }
        }

    }
}
