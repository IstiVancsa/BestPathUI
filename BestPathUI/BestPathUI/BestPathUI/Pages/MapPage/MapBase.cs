using BestPathUI.Pages.Components;
using Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        protected override async Task OnInitializedAsync()
        {
            Cities = new List<City>();//we might change this
            //await JSRuntime.InvokeVoidAsync("createMap");
        }

        protected void AddCity()
        {
            AddCityDialog.Show();
        }

        public async void AddCityDialog_OnDialogClose()
        {
            Cities = (await CitiesDataService.GetItemsAsync()).ToList();
            StateHasChanged();
        }
    }
}
