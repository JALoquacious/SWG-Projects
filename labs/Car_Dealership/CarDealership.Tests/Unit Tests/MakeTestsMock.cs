using CarDealership.DAL.Repositories.Mock;
using CarDealership.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDealership.Tests.Unit_Tests
{
    [TestFixture]
    public class MakeTestsMock
    {
        MakeRepositoryMock repo;
        List<Make> makes;

        [SetUp]
        public void SetUp()
        {
            repo = new MakeRepositoryMock();
            makes = repo.GetAll().ToList();

            if (makes.Count() > 4)
            {
                makes.RemoveAll(m => m.MakeId > 4);
            }
        }

        [Test]
        public void MockCanLoadMakes()
        {
            Assert.IsNotNull(makes);
            Assert.AreEqual(4, makes.Count());
        }

        [TestCase(1, "Chevrolet", "1/1/2001")]
        [TestCase(2, "Ford", "2/2/2002")]
        [TestCase(3, "Honda", "3/3/2003")]
        [TestCase(4, "Peugeot", "4/4/2004")]
        public void MockCanLoadMakeById(int id, string expectedName, string expectedDate)
        {
            var make = repo.GetById(id);

            Assert.IsNotNull(make);
            Assert.AreEqual(expectedName, make.Name);
            Assert.AreEqual(DateTime.Parse(expectedDate), make.DateAdded);
        }

        [Test]
        public void MockCanInsertMake()
        {
            var make = new Make()
            {
                Name = "Fiat",
                DateAdded = DateTime.Parse("5/5/2005")
            };

            repo.Insert(make);
            var makes = repo.GetAll().ToList();

            Assert.AreEqual(5, makes.Count());
            Assert.AreEqual("Fiat", makes[4].Name);
            Assert.AreEqual(DateTime.Parse("5/5/2005"), makes[4].DateAdded);
        }
    }
}
