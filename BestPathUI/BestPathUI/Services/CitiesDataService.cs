using Interfaces;
using Models.DTO;
using Models.Models;
using System.Net.Http;

namespace Services
{
    public class CitiesDataService : RestDataService<City, CityDTO>, ICitiesDataService
    {
        public CitiesDataService(HttpClient httpClient) : base(httpClient, "cities")
        {
            GetByFilterSelector = x => new City
            {
                Id = x.Id,
                CityName = x.CityName,
                DestinationPoint = x.DestinationPoint,
                MuseumType = x.MuseumType,
                NeedsHotel = x.NeedsHotel,
                NeedsMuseum = x.NeedsMuseum,
                NeedsRestaurant = x.NeedsRestaurant,
                RestaurantType = x.RestaurantType,
                StartPoint = x.StartPoint
            };
        }
    }
}
