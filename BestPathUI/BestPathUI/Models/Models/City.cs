using Models.DTO;

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
                StartPoint = this.StartPoint
            };
        }
    }
}
