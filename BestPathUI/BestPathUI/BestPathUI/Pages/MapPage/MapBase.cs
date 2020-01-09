using BestPathUI.Pages.Components;
using Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models.DTO;
using Models.Filters;
using Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestPathUI.Pages.MapPage
{
    public class MapBase : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }//this is used so we can call js methods inside our cs files
        [Inject]
        public ICitiesDataService CitiesDataService { get; set; }
        public IEnumerable<City> Cities { get; set; }
        protected AddCityDialog AddCityDialog { get; set; }
        public Models.Models.User User { get; set; }
        public IList<GoogleTextSearchDTO> RestaurantSearches { get; set; } = new List<GoogleTextSearchDTO>();
        public IList<GoogleTextSearchDTO> MuseumSearches { get; set; } = new List<GoogleTextSearchDTO>();
        protected override async Task OnInitializedAsync()
        {
            Cities = new List<City>();//we might change this
            await JSRuntime.InvokeVoidAsync("createMap");
            await JSRuntime.InvokeVoidAsync("initializeMap");
        }

        protected void AddCity()
        {
            AddCityDialog.Show();
        }

        protected async void SaveRoth()
        {
            await CitiesDataService.SavePathAsync(Cities);
        }

        protected async void GetLastRoth()
        {
            CityFilter cityFilter = new CityFilter { UserId = User.Id };
            Cities = (await CitiesDataService.GetItemsAsync(cityFilter.GetFilter())).ToList();
        }

        public void AddCityDialog_OnDialogClose(Map_AddCity map_AddCity)
        {
            this.RestaurantSearches = map_AddCity.RestaurantSearches;
            this.MuseumSearches = map_AddCity.MuseumSearches;
            var newList = Cities.ToList();
            newList.Add(map_AddCity.City);
            Cities = newList;
            StateHasChanged();
        }
    }
}
