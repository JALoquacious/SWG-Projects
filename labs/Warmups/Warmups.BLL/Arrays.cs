using System;
using System.Linq;

namespace Warmups.BLL
{
    public class Arrays
    {

        public bool FirstLast6(int[] numbers)
        {
            return numbers[0] == 6 || numbers[numbers.Length - 1] == 6;
        }

        public bool SameFirstLast(int[] numbers)
        {
            return numbers.Length >= 1 &&
                numbers[0] == numbers[numbers.Length - 1];
        }
        public int[] MakePi(int n)
        {
            // second try
            double pi = Math.PI;
            string piCharacters = pi.ToString().Remove(1, 1).Substring(0, n);
            int[] numbers = new int[n];

            for (int i = 0; i < n; i++)
            {
                numbers[i] = int.Parse(piCharacters.Substring(i, 1));
            }
            return numbers;

            // first try
            /*
            double pi = Math.PI;
            string piString = pi.ToString().Replace(".", String.Empty);
            char[] piArray = piString.ToCharArray();
            int[] numbers = new int[n];

            for (int i = 0; i < n; i++)
            {
                numbers[i] = (int)Char.GetNumericValue(piArray[i]);
                Console.Write(numbers[i]);
            }
            return numbers;
            */
        }

        public bool CommonEnd(int[] a, int[] b)
        {
            return (a[0] == b[0]) || (a[a.Length - 1] == b[b.Length - 1]);
        }
        
        public int Sum(int[] numbers)
        {
            int total = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                total += numbers[i];
            }
            return total;
        }
        // there is also a C# Enumerable.Sum method in LINQ
        // return numbers.Sum();

        public int[] RotateLeft(int[] numbers)
        {
            int temp;
            int len = numbers.Length - 1;
            
            for (int i = 0; i < len; i++)
            {
                temp = numbers[i];
                numbers[i] = numbers[i + 1];
                numbers[i + 1] = temp;
            }
            return numbers;
        }
        
        public int[] Reverse(int[] numbers)
        {
            int[] result = new int[numbers.Length];
            int j = 0; // opposite end of array from index i

            for (int i = numbers.Length - 1; i >= 0; i--)
            {
                result[j] = numbers[i];
                j++;
            }
            return result;
        }
        
        public int[] HigherWins(int[] numbers)
        {
            int maximum = numbers[0];

            if (numbers[numbers.Length - 1] > maximum)
            {
                maximum = numbers[numbers.Length - 1];
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = maximum;
            }
            return numbers;
        }
        
        public int[] GetMiddle(int[] a, int[] b)
        {
            return new int[] { a[a.Length / 2], b[b.Length / 2] };
        }
        
        public bool HasEven(int[] numbers)
        {
            var evens = numbers.Where(n => n % 2 == 0).ToList();
            return (evens.Count > 0);
        }
        
        public int[] KeepLast(int[] numbers)
        {
            int[] result = new int[numbers.Length * 2];
            result[result.Length - 1] = numbers[numbers.Length - 1];
            return result;
        }
        
        public bool Double23(int[] numbers)
        {
            int count2 = 0;
            int count3 = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == 2) count2++;
                if (numbers[i] == 3) count3++;
            }
            return count2 == 2 || count3 == 2;
        }
        
        public int[] Fix23(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == 2 && numbers[i + 1] == 3)
                {
                    numbers[i + 1] = 0;
                }
            }
            return numbers;
        }
        
        public bool Unlucky1(int[] numbers)
        {
            if (numbers.Length < 2) return false;

            for (int i = 0; i < 2; i++)
            {
                if (numbers[i] == 1 && numbers[i + 1] == 3) return true;
            }

            return numbers[numbers.Length - 2] == 1 &&
                   numbers[numbers.Length - 1] == 3;
        }
        
        public int[] Make2(int[] a, int[] b)
        {
            if (a.Length == 0)      return new int[] { b[0], b[1] };
            else if (a.Length == 1) return new int[] { a[0], b[0] };
            else                    return new int[] { a[0], a[1] };
        }

    }
}
