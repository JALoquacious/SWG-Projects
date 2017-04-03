using System;

namespace CarDealership.Models.Interfaces
{
    public interface IAuditable
    {
        DateTime CreatedOn { get; set; }
        DateTime UpdatedOn { get; set; }
        string CreatedBy { get; set; } // ASP.NET user class?
        string UpdatedBy { get; set; } // ASP.NET user class?
    }
}
