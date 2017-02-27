using System;
using FlooringBusiness.Models.Enums;

namespace FlooringBusiness.Models.Prototypes
{
    public class Request
    {
        public int OrderNumber { get; set; }
        public string Customer { get; set; }
        public string Product { get; set; }
        public string State { get; set; }
        public decimal Area { get; set; }
        public DateTime Date { get; set; }
        public WorkflowType Type { get; set; }
    }
}
