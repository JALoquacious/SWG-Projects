using CarDealership.DAL.Repositories.ADO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Tests.Integration_Tests
{
    [TestFixture]
    public class ColorTestsADO
    {
        [Test]
        public void ADOCanLoadInteriorColors()
        {
            var repo = new ColorRepositoryADO();
            var interiorColors = repo.GetAllInterior().ToList();

            Assert.AreEqual(7, interiorColors.Count);
            Assert.AreEqual(5, interiorColors[4].InteriorColorId);
            Assert.AreEqual("Beige", interiorColors[4].Name);
        }

        [Test]
        public void ADOCanLoadExteriorColors()
        {
            var repo = new ColorRepositoryADO();
            var exteriorColors = repo.GetAllExterior().ToList();

            Assert.AreEqual(13, exteriorColors.Count);
            Assert.AreEqual(12, exteriorColors[11].ExteriorColorId);
            Assert.AreEqual("DarkRed", exteriorColors[11].Name);
        }
    }
}
