using ShoppingCart.Domain.Abstract;
using ShoppingCart.Domain.Entities;
using System.Linq;
using System.Data.Entity;

namespace ShoppingCart.Domain.Concrete
{
    public class EFAddressRepository :IAddressRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        public IQueryable<Address> Addresses
        {
            get { return context.Addresses.Include(x=>x.City)
                ; }
        }

        public void SaveAddress(Address address) 
        {
            if (address.AddressId == 0)
            {
                context.Addresses.Add(address);
            }
            else
            {
                Address dbEntry = context.Addresses.Find(address.AddressId);
                if (dbEntry != null)
                {
                    dbEntry.AddressDescription = address.AddressDescription;
                    dbEntry.AddressLine1 = address.AddressLine1;
                    dbEntry.AddressLine2 = address.AddressLine2;
                    dbEntry.ApplicationUser = address.ApplicationUser;
                    dbEntry.City = address.City;
                    dbEntry.NameSurname = address.NameSurname;
                    dbEntry.Phone = address.Phone;
                    dbEntry.PhoneMobile = address.PhoneMobile;
                    dbEntry.PostalCode = address.PostalCode;
                }
            }
            context.SaveChanges();
        }

        public Address DeleteAddress(int addressID)
        {
            Address dbEntry = context.Addresses.Find(addressID);
            if (dbEntry != null)
            {
                context.Addresses.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
