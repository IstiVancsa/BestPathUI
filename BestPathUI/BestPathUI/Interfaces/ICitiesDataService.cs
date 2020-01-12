using Models.DTO;
using Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICitiesDataService : IRestDataService<City, CityDTO>
    {
        Task<bool> SavePathAsync(IEnumerable<City> cities);
    }
}
