using System.ComponentModel.DataAnnotations;

namespace Exercises.Attributes
{
    public class AbbreviationRuleAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string)
            {
                string abbreviation = (string) value;

                if (abbreviation.Length != 2)
                    return false;
                else
                    return true;
            }
            return false;
        }
    }
}