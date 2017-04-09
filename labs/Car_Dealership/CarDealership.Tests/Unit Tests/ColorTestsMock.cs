using CarDealership.DAL.Repositories.Mock;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Tests.Unit_Tests
{
    [TestFixture]
    public class ColorTestsMock
    {
        [Test]
        public void MockCanLoadInteriorColors()
        {
            var repo = new ColorRepositoryMock();
            var interiorColors = repo.GetAllInterior();

            Assert.IsNotNull(interiorColors);
            Assert.AreEqual(5, interiorColors.Count());
        }

        [Test]
        public void MockCanLoadExteriorColors()
        {
            var repo = new ColorRepositoryMock();
            var exteriorColors = repo.GetAllExterior();

            Assert.IsNotNull(exteriorColors);
            Assert.AreEqual(5, exteriorColors.Count());
        }
    }
}
