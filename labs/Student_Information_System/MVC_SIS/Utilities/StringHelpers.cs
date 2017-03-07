namespace Exercises.Utilities
{
    public static class StringHelpers
    {
        public static string ToTitle(this string input)
        {
            return input.Substring(0, 1).ToUpper() + input.Substring(1).ToLower();
        }
    }
}