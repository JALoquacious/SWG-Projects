using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealership.Models.Tables
{
    public class Sale
    {
        public Sale()
        {
            Date = DateTime.Now;
        }

        [Column(TypeName = "DATETIME2")]
        public DateTime Date         { get; set; }
        public int SaleId            { get; set; }
        public int CustomerId        { get; set; }
        public int PaymentTypeId     { get; set; }
        public string UserId         { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}
