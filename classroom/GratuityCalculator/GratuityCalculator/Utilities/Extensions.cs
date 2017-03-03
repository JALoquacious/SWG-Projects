namespace GratuityCalculator.Utilities
{
    public static class Extensions
    {
        public static bool ContainsAny(this string given, params string[] validStrings)
        {
            foreach (string text in validStrings)
            {
                if (given.Contains(text))
                    return true;
            }
            return false;
        }
    }
}