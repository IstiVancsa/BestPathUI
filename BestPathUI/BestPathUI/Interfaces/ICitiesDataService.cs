using Models.DTO;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ICitiesDataService : IRestDataService<City, CityDTO>
    {
    }
}
