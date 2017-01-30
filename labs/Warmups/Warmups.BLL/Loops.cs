using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Warmups.BLL
{
    public class Loops
    {

        public string StringTimes(string str, int n)
        {
            string originalString = str;

            for (int i = 1; i < n; i++)
            {
                str += originalString;
            }
            return str;
        }

        public string FrontTimes(string str, int n)
        {
            int count = (str.Length < 3) ? str.Length : 3;
            string result = String.Empty;

            for (int i = 0; i < n; i++)
            {
                result += str.Substring(0, count);
            }
            return result;
        }

        public int CountXX(string str)
        {
            int xxCount = 0;
            for (int i = 0; i < str.Length - 1; i++)
            {
                if (str[i] == 'x' && str[i + 1] == 'x')
                {
                    xxCount++;
                }
            }
            return xxCount;
        }

        public bool DoubleX(string str)
        {
            int firstX = str.IndexOf("x");
            if (firstX >= 0 && firstX < str.Length - 1)
            {
                return str[firstX + 1] == 'x';
            }
            return false;
        }

        public string EveryOther(string str)
        {
            int i = 0;
            string result = "";

            while (i < str.Length)
            {
                result += str[i];
                i += 2;
            }
            return result;
        }

        public string StringSplosion(string str)
        {
            string result = "";
            for (int i = 0; i <= str.Length; i++)
            {
                result += str.Substring(0, i);
            }
            return result;
        }

        public int CountLast2(string str)
        {
            int count = 0;
            string end2 = str.Substring(str.Length - 2, 2);

            for (int i = 0; i < str.Length - 2; i++)
            {
                if (str.Substring(i, 2) == end2) count++;
            }
            return count;
        }

        public int Count9(int[] numbers)
        {
            int count = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == 9) count++;
            }
            return count;
        }

        public bool ArrayFront9(int[] numbers)
        {
            int[] subset = numbers.Take(4).ToArray();
            return (Array.IndexOf(subset, 9) > -1);
        }

        public bool Array123(int[] numbers)
        {
            string numString = String.Join("", numbers);
            return numString.IndexOf("123") > -1;
        }

        public int SubStringMatch(string a, string b)
        {
            int count = 0;
            int shorter = Math.Min(a.Length, b.Length);

            for (int i = 0; i < shorter - 1; i++)
            {
                if (a.Substring(i, 2) == b.Substring(i, 2)) count++;
            }
            return count;
        }

        public string StringX(string str)
        {
            string result = "";
            for (int i = 0; i < str.Length; i++)
            {
                if ( !(i > 0 && i < str.Length - 2 && str[i] == 'x') )
                {
                    result += str[i];
                }
            }
            return result;
        }

        public string AltPairs(string str)
        {
            string result = "";
            for (int i = 0; i < str.Length; i += 4)
            {
                result += str[i];
                if (i + 1 < str.Length)
                {
                    result += str[i + 1];
                }
            }
            return result; 
        }

        public string DoNotYak(string str)
        {
            string toErase = "yak";
            int eraseLength = toErase.Length;
            int found = str.IndexOf(toErase);

            while (found != -1)
            {
                str = str.Substring(0, found) + str.Substring(found + eraseLength);
                found = str.IndexOf(toErase);
            }
            return str;

            // easy way:
            // return new Regex("yak").Replace(str, "");
        }

        public int Array667(int[] numbers)
        {
            int count = 0;
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] == 6 &&
                    numbers[i + 1] == 6 || numbers[i + 1] == 7)
                {
                    count++;
                }
            }
            return count;
        }

        public bool NoTriples(int[] numbers)
        {
            for (int i = 0; i < numbers.Length - 2; i++)
            {
                if (numbers[i] == numbers[i + 1] &&
                    numbers[i + 1] == numbers[i + 2])
                {
                    return false;
                }
            }
            return true;
        }

        public bool Pattern51(int[] numbers)
        {
            if (numbers.Length < 3) return false;

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] + 5 == numbers[i + 1] &&
                    numbers[i] - 1 == numbers[i + 2])
                {
                    return true;
                }
            }
            return false;
        }

    }
}
