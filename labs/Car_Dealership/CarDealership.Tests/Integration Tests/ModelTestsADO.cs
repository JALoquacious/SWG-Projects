using CarDealership.DAL.Repositories.ADO;
using CarDealership.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Tests.Integration_Tests
{
    [TestFixture]
    public class ModelTestsADO
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(
                ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString
                )
            )
            {
                var cmd = new SqlCommand()
                {
                    CommandText = "DbReset",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = cn
                };

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void ADOCanLoadModels()
        {
            var repo = new ModelRepositoryADO();
            var models = repo.GetAll().ToList();

            Assert.AreEqual(33, models.Count);
            Assert.AreEqual(1, models[0].ModelId);
            Assert.AreEqual("A4", models[0].Name);
        }

        [Test]
        public void ADOCanAddModel()
        {
            var repo = new ModelRepositoryADO();
            var model = new Model();
            
            model.UserId = "00000000-0000-0000-0000-000000000000";
            model.MakeId = 4;
            model.Year = 2012;
            model.Name = "Mustang";

            repo.Insert(model);

            Assert.IsNotNull(model);
            Assert.AreEqual(34, model.ModelId);
            Assert.AreEqual(4, model.MakeId);
            Assert.AreEqual(2012, model.Year);
            Assert.AreEqual("Mustang", model.Name);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", model.UserId);
        }
    }
}
