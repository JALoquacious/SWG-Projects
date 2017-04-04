using CarDealership.DAL.Repositories.Mock;
using CarDealership.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDealership.Tests.Unit_Tests
{
    [TestFixture]
    public class ModelTestsMock
    {
        ModelRepositoryMock repo;
        List<Model> models;

        [SetUp]
        public void SetUp()
        {
            repo = new ModelRepositoryMock();
            models = repo.GetAll().ToList();

            if (models.Count() > 5)
            {
                models.RemoveAll(m => m.ModelId > 5);
            }
        }

        [Test]
        public void MockCanLoadModels()
        {
            Assert.IsNotNull(models);
            Assert.AreEqual(5, models.Count());
        }

        [TestCase(1, "GT-R", "1/1/2001")]
        [TestCase(2, "Pilot", "2/2/2002")]
        [TestCase(3, "Solara", "3/3/2003")]
        [TestCase(4, "Century", "4/4/2004")]
        [TestCase(5, "Fiesta", "5/5/2005")]
        public void MockCanLoadModelById(int id, string expectedName, string expectedDate)
        {
            var model = repo.GetById(id);

            Assert.IsNotNull(model);
            Assert.AreEqual(expectedName, model.Name);
            Assert.AreEqual(DateTime.Parse(expectedDate), model.DateAdded);
        }

        [Test]
        public void MockCanInsertModel()
        {
            var model = new Model()
            {
                Name = "Challenger",
                DateAdded = DateTime.Parse("6/6/2006")
            };

            repo.Insert(model);
            models = repo.GetAll().ToList();

            Assert.AreEqual(6, models.Count());
            Assert.AreEqual("Challenger", models[5].Name);
            Assert.AreEqual(DateTime.Parse("6/6/2006"), models[5].DateAdded);
        }
    }
}
