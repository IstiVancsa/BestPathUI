using Models.DTO;
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
                Location = this.Location
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
        }
    }
}
