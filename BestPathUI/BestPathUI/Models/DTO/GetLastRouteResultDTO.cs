using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DTO
{
    public class GetLastRouteResult
    {
        public List<City> Cities { get; set; }
        public string Token { get; set; }
    }
}
