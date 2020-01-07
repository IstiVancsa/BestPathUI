using Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Interfaces;

namespace Services
{
    public class GoogleDataService : IGoogleDataService
    {
        private HttpClient _httpClient;

        public GoogleDataService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<IList<GoogleTextSearchDTO>> TextSearch(string searchText, LocationDTO locationDTO)
        {
            var items = new List<GoogleTextSearchDTO>();
            try
            {
                var response = await this._httpClient.GetAsync("https://maps.googleapis.com/maps/api/place/textsearch/json?query=" + searchText + "&sensor=true&location=" + locationDTO.lat.ToString() + ",%20" + locationDTO.lng.ToString() + "&radius=0.1&key=AIzaSyAIztt-WDuygbYykfzV8akJ3DyR-jrhJR");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<GoogleTextSearchDTO>>(content);
                }
            }
            catch (Exception ex)
            {
                var dsad = ex.Message;
            }
            return items;
        }
    }
}
