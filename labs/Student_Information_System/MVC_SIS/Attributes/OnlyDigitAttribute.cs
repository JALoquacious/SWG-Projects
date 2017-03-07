using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Exercises.Attributes
{
    public class OnlyDigitAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string)
            {
                Regex pattern = new Regex(@"^\d+$");
                string text = (string)value;

                if (pattern.IsMatch(text))
                    return true;

                return false;
            }
            return false;
        }
    }
}