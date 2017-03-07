using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Exercises.Attributes
{
    public class ZipLengthAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string)
            {
                string zip = (string) value;

                if (zip.Length != 5)
                    return false;
                else
                    return true;
            }
            return false;
        }
    }
}