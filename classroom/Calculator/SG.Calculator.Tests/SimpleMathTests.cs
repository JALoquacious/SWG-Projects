using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SG.Calculator.BLL;

namespace SG.Calculator.Tests
{
    [TestFixture]
    class SimpleMathTests
    {
        [Test]
        public void IntegerDivision1()
        {
            SimpleMath math = new SimpleMath();
            int result = math.Divide(5, 2);

            Assert.AreEqual(2, result);
        }

        [Test]
        public void DivideByZeroTest()
        {
            SimpleMath math = new SimpleMath();
            Assert.Throws<DivideByZeroException>(() => math.Divide(5, 0));
        }
    }
}