using System;
using System.Collections.Generic;
using System.Text;
using EmployeesMobile;
using Interfaces;
using Models.DTO;

namespace Services
{
    public class ReviewDataService : RestDataService<ReviewDTO>, IReviewDataService
    {
        public ReviewDataService() : base("Review")
        {

        }
    }
}
