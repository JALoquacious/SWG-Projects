using System;
using System.Linq;

namespace Warmups.BLL
{
    public class Strings
    {

        public string SayHi(string name)
        {
            return "Hello " + name + "!";
        }

        public string Abba(string a, string b)
        {
            return $"{a}{b}{b}{a}";
        }

        public string MakeTags(string tag, string content)
        {
            return $"<{tag}>{content}</{tag}>";
        }

        public string InsertWord(string container, string word) {
            return container.Substring(0, 2) + word +
                container.Substring(container.Length - 2, 2);
        }

        public string MultipleEndings(string str)
        {
            string end2 = str.Substring(str.Length - 2);
            return String.Join("", Enumerable.Repeat(end2, 3));
        }

        public string FirstHalf(string str)
        {
            return str.Substring(0, str.Length / 2);
        }

        public string TrimOne(string str)
        {
            return str.Substring(1, str.Length - 2);
        }

        public string LongInMiddle(string a, string b)
        {
            return (a.Length < b.Length) ? (a + b + a) : (b + a + b);
        }

        public string RotateLeft2(string str)
        {
            return str.Substring(2) + str.Substring(0, 2);
        }

        public string RotateRight2(string str)
        {
            return str.Substring(str.Length - 2) + str.Substring(0, str.Length - 2);
        }

        public string TakeOne(string str, bool fromFront)
        {
            return (fromFront) ? str.Substring(0, 1) : str.Substring(str.Length - 1);
        }

        public string MiddleTwo(string str)
        {
            return str.Substring(((str.Length / 2) - 1), 2);
        }

        public bool EndsWithLy(string str)
        {
            return (str.Length > 2)
                ? str.Substring(str.Length - 2) == "ly"
                : false;
            // alternately:
            // return str.EndsWith("ly");
        }

        public string FrontAndBack(string str, int n)
        {
            return str.Substring(0, n) + str.Substring(str.Length - n);
        }

        public string TakeTwoFromPosition(string str, int n)
        {
            return (n <= 0 || n + 2 > str.Length)
                ? str.Substring(0, 2)
                : str.Substring(n, 2);
        }

        public bool HasBad(string str)
        {
            return (str.Length < 2)
                ? false
                : str.Substring(0, 3).Equals("bad") ||
                  str.Substring(1, 3).Equals("bad");
        }

        public string AtFirst(string str)
        {
            while (str.Length < 2) str += "@";
            return str.Substring(0, 2);
        }

        public string LastChars(string a, string b)
        {
            if (String.IsNullOrEmpty(a)) a = "@";
            if (String.IsNullOrEmpty(b)) b = "@";
            return a.Substring(0, 1) + b.Substring(b.Length - 1);
        }

        public string ConCat(string a, string b)
        {
            if (a == "" || b == "" || a[a.Length - 1] != b[0]) return a + b;
            else return a + b.Substring(1);
        }

        public string SwapLast(string str)
        {
            if (str.Length < 2) return str;
            return str.Remove(str.Length - 2, 1) +
                str.Substring(str.Length - 2, 1);
        }

        public bool FrontAgain(string str)
        {
            return str.Substring(0, 2).Equals(str.Substring(str.Length - 2));
        }

        public string MinCat(string a, string b)
        {
            if (a.Length == b.Length) return a + b;
            return (a.Length > b.Length)
                ? a.Substring(a.Length - b.Length) + b
                : a + b.Substring(b.Length - a.Length);
        }

        public string TweakFront(string str)
        {
            if (str == "" || str[0] == 'a' && str[1] == 'b') return str;
            if (str[0] != 'a' && str[1] != 'b') return str.Substring(2);
            return (str[0] == 'a')
                ? str[0] + str.Substring(2) : str[1] + str.Substring(2);
        }

        public string StripX(string str)
        {
            if (str.Length < 2)
            {
                return String.Empty;
            }
            else if (str.Substring(0, 1) == "x" &&
                     str.Substring(str.Length - 1) == "x")
            {
                return str.Substring(1, str.Length - 2);
            }
            else if (str.Substring(0, 1) == "x")
            {
                return str.Substring(1);
            }
            else if (str.Substring(str.Length - 1) == "x")
            {
                return str.Substring(0, str.Length - 1);
            }
            else
            {
                return str;
            }
        }
    }
}
