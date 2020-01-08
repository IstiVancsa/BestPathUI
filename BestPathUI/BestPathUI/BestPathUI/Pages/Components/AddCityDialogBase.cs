using BestPathUI.Persistence;
using Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models.DTO;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestPathUI.Pages.Components
{
    public class AddCityDialogBase : ComponentBase
    {
        [Inject]
        public IGoogleDataService GoogleDataService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }//this is used so we can call js methods inside our cs files
        public City City { get; set; } = new City();
        public List<string> RestaurantTypes { get; set; }
        public List<string> MuseumTypes { get; set; }
        [Parameter]//now we can cann it as parameter in razor file
        public EventCallback<City> CloseEventCallBack { get; set; }//we are sending a message from adduserdialog to users overview

        public bool ShowDialog { get; set; }

        protected override void OnInitialized()
        {
            RestaurantTypes = new List<string>
            {
                "Mexican",
                "Italian",
                "Romanian",
                "France",
                "American",
                "Chinese"
            };

            MuseumTypes = new List<string>
            {
                "General",
                "Natural History",
                "Natural Science",
                "History",
                "Art",
                "Virtual"
            };
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //await JSRuntime.InvokeVoidAsync("createLocationAutocomplete");
            await JSRuntime.InvokeVoidAsync("initializeAutocompletes");
            //await JSRuntime.InvokeVoidAsync("initializeRestaurantAutocomplete");
            //await JSRuntime.InvokeVoidAsync("initializeMuseumAutocomplete");
        }
        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        private void ResetDialog()
        {
            this.City = new City
            {
                CityName = "",
                DestinationPoint = false,
                MuseumType = "",
                NeedsHotel = false,
                NeedsMuseum = false,
                NeedsRestaurant = false,
                RestaurantType = "",
                StartPoint = false
            };
        }

        protected async void RestaurantClicked(ChangeEventArgs restaurantEvent)
        {
            var result = await GoogleDataService.TextSearch(restaurantEvent.Value.ToString() + "+Restaurant", this.City.Location);
            Console.WriteLine("Do something");
        }

        protected async void MuseumClicked(ChangeEventArgs restaurantEvent)
        {
            var result = await GoogleDataService.TextSearch(restaurantEvent.Value.ToString() + "+Museum", this.City.Location);
            Console.WriteLine("Do something");
        }

        protected async Task HandleValidSubmit()
        {
            if(this.City.NeedsHotel)
            {
                Console.WriteLine("show hotels in a table and a marker on the map");
            }
            if (this.City.NeedsMuseum)
            {
                Console.WriteLine("show museums in a table and a marker on the map");
            }
            if (this.City.NeedsRestaurant)
            {
                Console.WriteLine("show restaurants in a table and a marker on the map");
            }
            await CloseEventCallBack.InvokeAsync(City);//we can send even the save employee here
            ShowDialog = false;

            StateHasChanged();
        }

        public async void SetLat(string value)
        {
            this.City.Location.lat = Convert.ToDouble(value);
            await Task.Delay(1);
            StateHasChanged();
        }
    }
}
