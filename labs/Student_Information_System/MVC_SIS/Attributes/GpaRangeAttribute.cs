using System.ComponentModel.DataAnnotations;

namespace Exercises.Attributes
{
    public class GpaRangeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is decimal)
            {
                decimal gpa = (decimal) value;

                if (gpa > 4 || gpa < 0)
                    return false;
                else
                    return true;
            }
            return false;
        }
    }
}