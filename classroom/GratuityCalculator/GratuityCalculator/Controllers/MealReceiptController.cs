using System;
using System.Web.Mvc;
using GratuityCalculator.Models;

namespace GratuityCalculator.Controllers
{
    public class MealReceiptController : Controller
    {
        [HttpPost]
        public ActionResult Result(MealReceipt receipt)
        {
            receipt.Date = DateTime.Now;
            receipt.TaxAmount = Calculator.CalculateTax(receipt.TaxRate, receipt.SubTotal);
            receipt.Gratuity = Calculator.CalculateTip(receipt.Description, receipt.SubTotal);
            receipt.Total = Calculator.CalculateTotal(receipt.SubTotal, receipt.TaxAmount, receipt.Gratuity);

            return View(receipt);
        }
    }
}