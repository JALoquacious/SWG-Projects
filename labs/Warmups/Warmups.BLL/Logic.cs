using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warmups.BLL
{
    public class Logic
    {

        public bool GreatParty(int cigars, bool isWeekend)
        {
            return (isWeekend && cigars >= 40) || (40 <= cigars && cigars <= 60);
        }
        
        public int CanHazTable(int yourStyle, int dateStyle)
        {
            if (yourStyle <= 2 || dateStyle <= 2)      return 0;
            else if (yourStyle >= 8 || dateStyle >= 8) return 2;
            else                                       return 1;
        }

        public bool PlayOutside(int temp, bool isSummer)
        {
            int lowTemp = 60;
            int highTemp = (isSummer) ? 100 : 90;
            return (lowTemp <= temp) && (temp <= highTemp);
        }
        
        public int CaughtSpeeding(int speed, bool isBirthday)
        {
            if (isBirthday) speed -= 5;
            return (speed > 80 ?
                2 : speed > 60 ?
                1 : 0);
        }
        
        public int SkipSum(int a, int b)
        {
            int sum = a + b;
            return (sum >= 13 && sum <= 19) ? 20 : sum;
        }
        
        public string AlarmClock(int day, bool vacation)
        {
            if (vacation) return (1 <= day && day <= 5) ? "10:00" : "off";
            else          return (1 <= day && day <= 5) ? "7:00" : "10:00";
        }
        
        public bool LoveSix(int a, int b)
        {
            return a == 6 || b == 6 || a + b == 6 || Math.Abs(a - b) == 6;
        }
        
        public bool InRange(int n, bool outsideMode)
        {
            return (outsideMode) ? (n <= 1 || n >= 10) : (n >= 0 && n <= 10);
        }
        
        public bool SpecialEleven(int n)
        {
            return (n % 11 == 0) || (n % 11 == 1);
        }
        
        public bool Mod20(int n)
        {
            return (n % 20 == 1) || (n % 20 == 2);
        }
        
        public bool Mod35(int n)
        {
            return (n % 3 == 0) ^ (n % 5 == 0);
            // less magical but more common alternate way:
            // return (n % 3 == 0 || n % 5 == 0) && !(n % 3 == 0 && n % 5 == 0);
        }

        public bool AnswerCell(bool isMorning, bool isMom, bool isAsleep)
        {
            return (isAsleep) ? false : (isMorning && !isMom) ? false : true;
        }
        
        public bool TwoIsOne(int a, int b, int c)
        {
            return (a + b == c) || (a + c == b) || (b + c == a);
        }
        
        public bool AreInOrder(int a, int b, int c, bool bOk)
        {
            return (bOk)
                ? (b < c)
                : (a < b) && (b < c);
        }
        
        public bool InOrderEqual(int a, int b, int c, bool equalOk)
        {
            return (equalOk)
                ? (a <= b) && (b <= c)
                : (a < b) && (b < c);
        }
        
        public bool LastDigit(int a, int b, int c)
        {
            return (a % 10 == b % 10) ||
                   (a % 10 == c % 10) ||
                   (b % 10 == c % 10);
        }
        
        public int RollDice(int die1, int die2, bool noDoubles)
        {
            if (noDoubles && die1 == die2)
            {
                return (die1 + 1 == 6) ? (++die1 % 5 + die2) : (++die1 + die2);
            }
            return die1 + die2;
        }

    }
}
