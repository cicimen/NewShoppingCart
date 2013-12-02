using System.Linq;
using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Domain.Abstract
{
    public interface IAddressRepository
    {
        IQueryable<Address> Addresses { get; }

        void SaveAddress(Address address);

        Address DeleteAddress(int addressID);
    }
}
