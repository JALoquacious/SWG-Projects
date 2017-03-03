using GratuityCalculator.Utilities;

namespace GratuityCalculator.Models
{
    public static class Calculator
    {
        public static decimal CalculateTax(decimal taxRate, decimal subTotal)
        {
            return subTotal * (taxRate / 100);
        }

        public static decimal CalculateTip(string description, decimal subTotal)
        {
            decimal low = 15, med = 18, high = 22;
            decimal tipRate;

            if (description.ContainsAny("bad", "poor", "slow", "disappointing", "inferior", "careless"))
            {
                tipRate = low;
            }
            else if (description.ContainsAny("good", "mediocre", "average", "ordinary", "decent", "unremarkable"))
            {
                tipRate = med;
            }
            else if (description.ContainsAny("excellent", "superb", "delightful", "outstanding", "amazing", "top-notch"))
            {
                tipRate = high;
            }
            else
            {
                tipRate = med;
            }
            return subTotal * (tipRate / 100);
        }

        public static decimal CalculateTotal(decimal subTotal, decimal taxAmount, decimal tipAmount)
        {
            return subTotal + taxAmount + tipAmount;
        }
    }
}