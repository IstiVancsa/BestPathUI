using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DTO
{
    public class GetLastRouteResult
    {
        public List<Tuple<DateTime, List<City>>> Cities { get; set; }
        public string Token { get; set; }
        public GetLastRouteResult()
        {
            this.Cities = new List<Tuple<DateTime, List<City>>>();
        }
    }
}
