using Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Models.DTO;
using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CitiesDataService : RestDataService<City, CityDTO>, ICitiesDataService
    {
        public CitiesDataService(HttpClient httpClient, IConfiguration configuration, IJSRuntime JSRuntime) : base(httpClient, configuration, JSRuntime, "cities")
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
                StartPoint = x.StartPoint,
                Location = x.Location,
                UserId = x.UserId
            };
        }

        public async Task<GetLastRouteResult> GetLastRoute(string filters)
        {
            return (await this.ReturnGetHttp<GetLastRouteResult>(this.UrlApi + "GetLastRoute/" + filters));
        }

        public async Task<bool> SavePathAsync(IEnumerable<City> cities)
        {
            bool result = true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.Token);

                IList<CityDTO> citiesDTO = new List<CityDTO>();
                foreach (var city in cities)
                    citiesDTO.Add(city.GetDTO());

                var content = new StringContent(JsonConvert.SerializeObject(citiesDTO), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(this.UrlApi + "AddCities", content);

                result = response.IsSuccessStatusCode;
            }

            return result;
        }
    }
}
