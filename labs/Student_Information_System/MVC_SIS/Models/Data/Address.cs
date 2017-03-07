using Exercises.Attributes;

namespace Exercises.Models.Data
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public State State { get; set; }

        // must create new ViewModel to be able to use this attribute here
        //[ZipLength(ErrorMessage = "Postal code must be 5 digits.")]
        public string PostalCode { get; set; }
    }
}