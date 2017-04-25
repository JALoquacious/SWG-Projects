using DVDLibrary.Data.Repositories;
using NUnit.Framework;
using System.Configuration;
using System.Data.SqlClient;

namespace DVDLibrary.Tests.IntegrationTests
{
    [TestFixture]
    public class EFRepoTests
    {
        [SetUp]
        public void Init()
        {
            var conn = new SqlConnection(
                ConfigurationManager
                    .ConnectionStrings["EFConnection"]
                    .ConnectionString
            );

            using (conn)
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void EFNotFoundDvdReturnsNull()
        {
            var repo = new EFRepository();
            var dvd = repo.GetDvdById(9999);

            Assert.IsNull(dvd);
        }

        [Test]
        public void EFCanLoadDvds()
        {
            var repo = new EFRepository();
            var dvds = repo.GetAllDvds();

            Assert.AreEqual(6, dvds.Count);
            Assert.AreEqual(1, dvds[0].Id);
            Assert.AreEqual("Back to the Future", dvds[0].Title);
        }

        [TestCase(1, "Back to the Future")]
        [TestCase(2, "The Wolf of Wall Street")]
        [TestCase(3, "Inception")]
        public void EFCanGetDvdById(int id, string expectedTitle)
        {
            var repo = new EFRepository();
            var actual = repo.GetDvdById(id);

            Assert.AreEqual(expectedTitle, actual.Title);
        }

        [TestCase("Back to the Future", 1)]
        [TestCase("The Wolf of Wall Street", 2)]
        [TestCase("Inception", 3)]
        public void EFCanGetDvdByTitle(string title, int expectedId)
        {
            var repo = new EFRepository();
            var actual = repo.GetDvdsByTitle(title);

            Assert.AreEqual(expectedId, actual[0].Id);
        }

        [TestCase(1985, 1)]
        [TestCase(2013, 2)]
        [TestCase(2010, 3)]
        public void EFCanGetDvdByReleaseYear(int releaseYear, int expectedId)
        {
            var repo = new EFRepository();
            var actual = repo.GetDvdsByReleaseYear(releaseYear);

            Assert.AreEqual(expectedId, actual[0].Id);
        }

        [TestCase("Ridley Scott", "Alien")]
        [TestCase("Roger Allers & Rob Minkoff", "The Lion King")]
        public void EFCanGetDvdsByDirector(string director, string expectedTitle)
        {
            var repo = new EFRepository();
            var actual = repo.GetDvdsByDirector(director);

            Assert.AreEqual(expectedTitle, actual[0].Title);
        }
    }
}
