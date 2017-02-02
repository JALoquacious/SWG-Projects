using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Factorizor.BLL;

namespace Factorizor.Tests
{
    [TestFixture]
    public class FactorizorTests
    {
        [TestCase(6, new int[] { 1, 2, 3 }, false)]
        [TestCase(17, new int[] { 1 }, true)]
        [TestCase(15, new int[] { 1, 3, 5 }, false)]
        public void PrimeNumberTest(int number, int[] factors, bool expected)
        {
            Calculator calc = new Calculator();
            bool actual = calc.IsPrimeNumber(number, factors);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(6, new int[] {1, 2, 3}, true)]
        [TestCase(28, new int[] { 1, 2, 4, 7, 14 }, true)]
        [TestCase(496, new int[] { 1, 2, 4, 8, 16, 31, 62, 124, 248 }, true)]
        [TestCase(10, new int[] { 1, 2, 5 }, false)]
        [TestCase(99, new int[] { 1, 3, 9, 11, 33 }, false)]
        public void PerfectNumberTest(int number, int[] factors, bool expected)
        {
            Calculator calc = new Calculator();
            bool actual = calc.IsPerfectNumber(number, factors);
            Assert.AreEqual(expected, actual);
        }
    }
}
