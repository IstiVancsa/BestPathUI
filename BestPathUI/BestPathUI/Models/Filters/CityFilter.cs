using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Filters
{
    public class CityFilter : BaseFilterModel
    {
        public Guid UserId { get; set; }
    }
}
