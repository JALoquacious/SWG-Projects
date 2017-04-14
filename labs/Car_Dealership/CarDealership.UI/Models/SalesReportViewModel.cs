using CarDealership.Models.Queries;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CarDealership.UI.Models
{
    public class SalesReportViewModel
    {
        public string UserId                          { get; set; }
        public DateTime? FromDate                     { get; set; }
        public DateTime? ToDate                       { get; set; }
        public IEnumerable<SelectListItem> Users      { get; set; }
        public IEnumerable<SalesReportQueryRow> Sales { get; set; }
    }
}