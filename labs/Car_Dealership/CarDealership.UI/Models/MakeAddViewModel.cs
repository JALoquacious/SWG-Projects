using CarDealership.Models.Queries;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.UI.Models
{
    public class MakeAddViewModel
    {
        [Required(ErrorMessage = "An automaker's name is required.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Za-z \-\']$")]
        public string NewMakeName { get; set; }

        public IEnumerable<MakeUserQueryRow> MakeUserTable { get; set; }
    }
}