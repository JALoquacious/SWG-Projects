using CarDealership.Models.Queries;
using System.Collections.Generic;

namespace CarDealership.UI.Models
{
    public class UserReportViewModel
    {
        public IEnumerable<UserReportQueryRow> Users { get; set; }
    }
}