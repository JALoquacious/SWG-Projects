using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Warmups.BLL
{
    public class Conditionals
    {
        public bool AreWeInTrouble(bool aSmile, bool bSmile)
        {
            return !(aSmile ^ bSmile); // aSmile == bSmile
        }

        public bool CanSleepIn(bool isWeekday, bool isVacation)
        {
            return !isWeekday || isVacation;
        }

        public int SumDouble(int a, int b)
        {
            return (a == b) ? 2 * (a + b) : a + b;
        }

        public int Diff21(int n)
        {
            return (n < 21) ? 21 - n : 2 * (n - 21);
        }

        public bool ParrotTrouble(bool isTalking, int hour)
        {
            return isTalking && (hour < 7 || hour > 20);
        }

        public bool Makes10(int a, int b)
        {
            return a == 10 || b == 10 || a + b == 10;
        }

        public bool NearHundred(int n)
        {
            return (Math.Abs(100 - n) <= 10 || Math.Abs(200 - n) <= 10);
        }

        public bool PosNeg(int a, int b, bool negative)
        {
            return (negative) ? (a < 0 && b < 0) : (a < 0 ^ b < 0);
        }

        public string NotString(string s)
        {
            return (s.StartsWith("not ") ? s : "not " + s);
        }

        public string MissingChar(string str, int n)
        {
            return str.Remove(n, 1);
        }

        public string FrontBack(string str)
        {
            if (str.Length == 1) return str;
            return str[str.Length - 1] + str.Substring(1, str.Length - 2) + str[0];
        }

        public string Front3(string str)
        {
            int len = str.Length < 3 ? str.Length : 3;
            return string.Concat(Enumerable.Repeat(str.Substring(0, len), 3));
        }

        public string BackAround(string str)
        {
            return str[str.Length - 1] + str + str[str.Length - 1];
        }

        public bool Multiple3or5(int number)
        {
            return number % 3 == 0 || number % 5 == 0;
        }

        public bool StartHi(string str)
        {
            char[] delimiters = { '\0', ' ', ',', '.', '!' };

            if (str == "hi") return true;
            else if (str[0] == 'h' && str[1] == 'i' && str[2] != '\0')
            {
                if (Array.IndexOf(delimiters, str[2]) > 0)
                {
                    return true;
                }
            }
            return false;

            // or the sensible way:
            // return new Regex("^hi$|hi[ ,.!]").IsMatch(str);
        }

        public bool IcyHot(int temp1, int temp2)
        {
            return (temp1 < 0 && temp2 > 100) ^ (temp1 > 100 && temp2 < 0);
        }

        public bool Between10and20(int a, int b)
        {
            return (a >= 10 && a <= 20) || (b >= 10 && b <= 20);
        }

        public bool HasTeen(int a, int b, int c)
        {
            int[] teens = { 13, 14, 15, 16, 17, 18, 19 };
            return teens.Contains(a) || teens.Contains(b) || teens.Contains(c);
        }

        public bool SoAlone(int a, int b)
        {
            int[] teens = { 13, 14, 15, 16, 17, 18, 19 };
            return teens.Contains(a) ^ teens.Contains(b);
        }

        public string RemoveDel(string str)
        {
            return str.Length > 3 && str.Substring(1, 3).Equals("del")
                ? str[0] + str.Substring(4)
                : str;
        }

        public bool IxStart(string str)
        {
            return new Regex(@"^.ix").IsMatch(str);
            // return str.Substring(1, 2).Equals("ix");
        }

        public string StartOz(string str)
        {
            string output = "";
            if (str.Length > 0 && str[0] == 'o') output += "o";
            if (str.Length > 1 && str[1] == 'z') output += "z";
            return output;
        }

        public int Max(int a, int b, int c)
        {
            return Math.Max(Math.Max(a, b), c);
        }

        public int Closer(int a, int b)
        {
            if (Math.Abs(a - 10) < Math.Abs(b - 10)) return a;
            if (Math.Abs(a - 10) > Math.Abs(b - 10)) return b;
            return 0;
        }

        public bool GotE(string str)
        {
            int numElements = str.Where(letter => letter == 'e').ToArray().Length;
            return (numElements >= 1 && numElements <= 3);
        }

        public string EndUp(string str)
        {
            int len = str.Length;
            if (len <= 3) return str.ToUpper();
            return str.Substring(0, len - 3) + str.Substring(len - 3).ToUpper();
        }

        public string EveryNth(string str, int n)
        {
            string output = "";
            for (int i = 0; i < str.Length; i += n)
            {
                output += str[i];
            }
            return output;
        }
    }
}