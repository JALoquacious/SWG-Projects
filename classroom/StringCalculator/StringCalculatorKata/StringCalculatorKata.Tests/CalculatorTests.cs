using NUnit.Framework;
using StringCalculatorKata.BLL;

namespace StringCalculatorKata.Tests
{
    public class CalculatorTests
    {
        [Test]
        public void EmptyStringReturnsZero()
        {
            Calculator calc = new Calculator();

            int actual = calc.Add("");

            Assert.AreEqual(0, actual);
        }

        [Test]
        public void SingleNumberReturnsNumber()
        {
            Calculator calc = new Calculator();

            int actual = calc.Add("5");

            Assert.AreEqual(5, actual);
        }

        [Test]
        public void MultipleNumbersWithCommasReturnsSum()
        {
            Calculator calc = new Calculator();

            int actual = calc.Add("1,2,3,4,5");

            Assert.AreEqual(15, actual);
        }

        [Test]
        public void MultipleNumbersWithNewLineReturnsSum()
        {
            Calculator calc = new Calculator();

            int actual = calc.Add("1\n2");

            Assert.AreEqual(3, actual);
        }

        [Test]
        public void MultipleNumbersWithCommaOrNewLineReturnsSum()
        {
            Calculator calc = new Calculator();

            int actual = calc.Add("1\n2,1\n\n1,3");

            Assert.AreEqual(8, actual);
        }

        [Test]
        public void CustomDelimiterReturnsSum()
        {
            Calculator calc = new Calculator();

            int actual = calc.Add("//[AB]\n1AB49AB2");

            Assert.AreEqual(52, actual);
        }
    }
}
