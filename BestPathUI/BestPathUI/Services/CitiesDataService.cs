using Interfaces;
using Models.DTO;
using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<bool> SavePathAsync(IEnumerable<City> cities)
        {
            bool result = true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(this.Token);

                IList<CityDTO> citiesDTO = new List<CityDTO>();
                foreach (var city in cities)
                    citiesDTO.Add(city.GetDTO());

                PathDTO path = new PathDTO { cities = citiesDTO, UserId = new Guid("42001e55-c6ec-4b56-8008-0d5930895867") };

                var content = new StringContent(JsonConvert.SerializeObject(citiesDTO), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(this.UrlApi + "AddCities", content);

                result = response.IsSuccessStatusCode;
            }

            return result;
        }
    }
}
