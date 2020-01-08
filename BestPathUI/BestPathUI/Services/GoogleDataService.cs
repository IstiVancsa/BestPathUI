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
        public async Task<GoogleTextSearchResultsDTO> TextSearch(string searchText, LocationDTO locationDTO)
        {
            var item = new GoogleTextSearchResultsDTO();
            var uri = new Uri("https://maps.googleapis.com/maps/api/place/textsearch/json?query=" + searchText + "&sensor=true&location=" + locationDTO.lat.ToString() + ",%20" + locationDTO.lng.ToString() + "&radius=0.1&key=AIzaSyAIztt-WDuygbYykfzV8akJ3DyR-jrhJRY");
            try
            {
                var response = await this._httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<GoogleTextSearchResultsDTO>(content);
                }
            }
            catch (Exception ex)
            {
                var dsad = ex.Message;
            }
            return item;
        }
    }
}
