using System.ComponentModel.DataAnnotations;

namespace Exercises.Models.Data
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        //[Required(ErrorMessage = "Please enter a STATE.")]
        public State State { get; set; }
        public string PostalCode { get; set; }
    }
}