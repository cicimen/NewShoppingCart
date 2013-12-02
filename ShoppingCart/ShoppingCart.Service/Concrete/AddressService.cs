using System.Collections.Generic;
using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;

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

        public void AddFor(Address address, ApplicationUser user)
        {
            _addresses.AddFor(address, user);
        }

        public void DeleteAddress(int id, ApplicationUser user)
        { 
            _addresses.DeleteAddress(id, user);
        }
    }
}
