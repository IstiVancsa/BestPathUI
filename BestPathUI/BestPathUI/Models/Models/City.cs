﻿using Models.DTO;
using System;

namespace Models.Models
{
    public class City : BaseModel
    {
        public string CityName { get; set; }
        public bool DestinationPoint { get; set; }
        public bool StartPoint { get; set; }
        public bool NeedsHotel { get; set; }
        public bool NeedsRestaurant { get; set; }
        public string RestaurantType { get; set; }
        public bool NeedsMuseum { get; set; }
        public string MuseumType { get; set; }
        public LocationDTO Location { get; set; }
        public GoogleTextSearchDTO SelectedRestaurant { get; set; }
        public GoogleTextSearchDTO SelectedMuseum { get; set; }
        public Guid UserId { get; set; }

        public new CityDTO GetDTO()
        {
            return new CityDTO
            {
                Id = this.Id,
                CityName = this.CityName,
                DestinationPoint = this.DestinationPoint,
                MuseumType = this.MuseumType,
                NeedsHotel = this.NeedsHotel,
                NeedsMuseum = this.NeedsMuseum,
                NeedsRestaurant = this.NeedsRestaurant,
                RestaurantType = this.RestaurantType,
                StartPoint = this.StartPoint,
                Location = this.Location,
                UserId = this.UserId
            };
        }
        public City()
        {
            Id = Guid.NewGuid();
            CityName = "";
            DestinationPoint = false;
            MuseumType = "";
            NeedsHotel = false;
            NeedsMuseum = false;
            NeedsRestaurant = false;
            RestaurantType = "";
            StartPoint = false;
            Location = new LocationDTO();
            SelectedMuseum = new GoogleTextSearchDTO();
            SelectedRestaurant = new GoogleTextSearchDTO();
            UserId = new Guid("42001e55-c6ec-4b56-8008-0d5930895867");
        }
    }
}
