using System;
using System.Collections.Generic;
using FlooringBusiness.Models.Enums;

namespace FlooringBusiness.Models.Prototypes
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public Order Order { get; set; }
        public List<Order> Orders { get; set; }
        public WorkflowType Type { get; set; }
    }
}
