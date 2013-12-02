using ShoppingCart.Domain.Abstract;
using ShoppingCart.Domain.Entities;
using System.Linq;

namespace ShoppingCart.Domain.Concrete
{
    public class EFCityRepository :ICityRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        public IQueryable<City> Cities
        {
            get { return context.Cities; }
        }

        public void SaveCity(City city)
        {
            if (city.CityID == 0)
            {
                context.Cities.Add(city);
            }
            else
            {
                City dbEntry = context.Cities.Find(city.CityID);
                if (dbEntry != null)
                {
                    dbEntry.CityCode = city.CityCode;
                    dbEntry.CityName = city.CityName;
                    dbEntry.DisplayOrder = city.DisplayOrder;
                }
            }
            context.SaveChanges();
        }
        public City DeleteCity(int cityID) 
        {
            City dbEntry = context.Cities.Find(cityID);
            if (dbEntry != null)
            {
                context.Cities.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
