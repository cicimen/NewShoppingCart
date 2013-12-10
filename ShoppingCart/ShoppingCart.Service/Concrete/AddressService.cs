using System.Collections.Generic;
using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using ShoppingCart.Service.ViewModel;
using System;

namespace ShoppingCart.Service
{
    public class AddressService : IAddressService
    {
        private readonly IContext _context;
        private readonly IAddressRepository _addresses;

        public AddressService(IContext context)
        {
            _context = context;
            _addresses = context.Addresses;
        }

        public IEnumerable<Address> GetFor(ApplicationUser user)
        {
            return _addresses.GetFor(user);
        }

        public void AddFor(AddressViewModel model, ApplicationUser user)
        {
            var address = new Address()
            {
                AddressDescription = model.AddressDescription,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                CityID = model.CityID,
                DateCreated = DateTime.Now,
                NameSurname = model.NameSurname,
                Phone = model.Phone,
                PhoneMobile = model.PhoneMobile,
                PostalCode = model.PostalCode
            };

            _addresses.AddFor(address, user);
            _context.SaveChanges();
        }

        public void DeleteAddress(int id, ApplicationUser user)
        { 
            _addresses.DeleteAddress(id, user);
        }
    }
}
