using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DTO
{
    public class PathDTO
    {
        public IList<CityDTO> cities { get; set; } = new List<CityDTO>();
        public Guid UserId { get; set; } = new Guid();
    }
}
