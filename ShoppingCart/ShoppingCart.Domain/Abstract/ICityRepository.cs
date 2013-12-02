using System.Linq;
using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Domain.Abstract
{
    public interface ICityRepository
    {
        IQueryable<City> Cities { get; }
        void SaveCity(City city);
        City DeleteCity(int cityID);
    }
}
