﻿using BestPathUI.Pages.Components;
using Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models.DTO;
using Models.Filters;
using Models.Models;
using System;
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
        public IList<City> Cities { get; set; }
        protected AddCityDialog AddCityDialog { get; set; }
        public Models.Models.User User { get; set; } = new Models.Models.User { Id = new Guid("42001e55-c6ec-4b56-8008-0d5930895867") };
        public IList<GoogleTextSearchDTO> RestaurantSearches { get; set; } = new List<GoogleTextSearchDTO>();
        public IList<GoogleTextSearchDTO> MuseumSearches { get; set; } = new List<GoogleTextSearchDTO>();
        protected override async Task OnInitializedAsync()
        {
            Cities = new List<City>();
        }
        private bool _mapInitialized { get; set; } = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!_mapInitialized)
            {
                _mapInitialized = true;
                await JSRuntime.InvokeVoidAsync("createMap");
                await JSRuntime.InvokeVoidAsync("initializeMap");
            }
        }

        protected void AddCity()
        {
            AddCityDialog.Show();
        }

        protected async void ShowRoute()
        {
            var startPoint = GetStartPointGeoCoordinates();
            var endPoint = GetDestinationPointGeoCoordinates();
            var intermediatePoints = GetIntermediatePointsGeoCoordinates();
            if(startPoint != null && endPoint != null)
                await JSRuntime.InvokeVoidAsync("showRoute", startPoint, endPoint, intermediatePoints);
        }

        protected async void NewRoute()
        {
            this.Cities.Clear();
            await JSRuntime.InvokeVoidAsync("removeDirections");
            StateHasChanged();
        }

        protected async void SaveRoute()
        {
            await CitiesDataService.SavePathAsync(Cities);
        }

        protected async void GetLastRoute()
        {
            CityFilter cityFilter = new CityFilter { UserId = User.Id };
            Cities = (await CitiesDataService.GetLastRoute(cityFilter.GetFilter())).ToList();
            ShowRoute();
        }

        protected void RestaurantSelected(GoogleTextSearchDTO restaurant)
        {
            this.Cities[Cities.Count() - 1].SelectedRestaurant = restaurant;
            this.RestaurantSearches.Clear();
            StateHasChanged();
        }

        protected void MuseumSelected(GoogleTextSearchDTO museum)
        {
            this.Cities[Cities.Count() - 1].SelectedMuseum = museum;
            this.MuseumSearches.Clear();
            StateHasChanged();
        }

        protected async void ShowLocation(GoogleTextSearchDTO place)
        {
            await JSRuntime.InvokeVoidAsync("showLocation", new LocationDTO { lat = place.geometry.location.lat, lng = place.geometry.location.lng });
        }

        protected async void HideLocation(GoogleTextSearchDTO place)
        {
            await JSRuntime.InvokeVoidAsync("hideLocation");
        }

        public void AddCityDialog_OnDialogClose(Map_AddCity map_AddCity)
        {
            this.RestaurantSearches = map_AddCity.RestaurantSearches;
            this.MuseumSearches = map_AddCity.MuseumSearches;
            this.Cities.Add(map_AddCity.City);
            //we have to set the user id of map_addCity.CIty
            StateHasChanged();
        }

        public LocationDTO GetStartPointGeoCoordinates()
        {
            return Cities.Where(x => x.StartPoint)
                         .Select(x => x.Location)
                         .FirstOrDefault();
        }

        public LocationDTO GetDestinationPointGeoCoordinates()
        {
            return Cities.Where(x => x.DestinationPoint)
                         .Select(x => x.Location)
                         .FirstOrDefault();
        }

        public IList<LocationDTO> GetIntermediatePointsGeoCoordinates()
        {
            return Cities.Where(x => !x.DestinationPoint && !x.StartPoint)
                         .Select(x => x.Location)
                         .ToList();
        }
    }
}
