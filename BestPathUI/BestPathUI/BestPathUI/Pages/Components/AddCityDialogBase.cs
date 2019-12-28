using Microsoft.AspNetCore.Components;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestPathUI.Pages.Components
{
    public class AddCityDialogBase : ComponentBase
    {
        public City City { get; set; } = new City
        {
            Id = Guid.NewGuid(),
            CityName = "",
            DestinationPoint = false,
            MuseumType = "",
            NeedsHotel = false,
            NeedsMuseum = false,
            NeedsRestaurant = false,
            RestaurantType = "",
            StartPoint = false
        };
        [Parameter]//now we can cann it as parameter in razor file
        public EventCallback<bool> CloseEventCallBack { get; set; }//we are sending a message from adduserdialog to users overview

        public bool ShowDialog { get; set; }
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

        protected async Task HandleValidSubmit()
        {
            await CloseEventCallBack.InvokeAsync(true);//we can send even the save employee here

            ShowDialog = false;

            StateHasChanged();
        }
    }
}
