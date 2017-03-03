using System;

namespace GratuityCalculator.Models
{
    public class MealReceipt
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Gratuity { get; set; }
        public decimal Total { get; set; }
    }
}