using CarDealership.DAL.Repositories.ADO;
using CarDealership.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace CarDealership.Tests.Integration_Tests
{
    [TestFixture]
    public class MakeTestsADO
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
        public void ADOCanLoadMakes()
        {
            var repo = new MakeRepositoryADO();
            var makes = repo.GetAll().ToList();

            Assert.AreEqual(8, makes.Count);
            Assert.AreEqual(1, makes[0].MakeId);
            Assert.AreEqual("Audi", makes[0].Name);
        }

        [Test]
        public void ADOCanInsertMake()
        {
            var repo = new MakeRepositoryADO();
            var make = new Make();

            //make.MakeId = 9;
            make.UserId = "00000000-0000-0000-0000-000000000000";
            make.Name = "Jaguar";

            repo.Insert(make);

            Assert.IsNotNull(make);
            Assert.AreEqual(9, make.MakeId);
            Assert.AreEqual("Jaguar", make.Name);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", make.UserId);
        }
    }
}
