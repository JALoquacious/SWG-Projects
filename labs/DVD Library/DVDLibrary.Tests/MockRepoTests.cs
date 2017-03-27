using DVDLibrary.Data.Repositories;
using NUnit.Framework;
using System.Linq;

namespace DVDLibrary.Tests
{
    [TestFixture]
    public class MockRepoTests
    {
        [Test]
        public void CanGetDvdListFromRepo()
        {
            var repo = new MockRepository();
            var dvds = repo.GetAllDvds();

            Assert.IsNotNull(dvds);
            Assert.AreEqual(3, dvds.ToList().Count());
        }

        [TestCase(1, "A Good Tale")]
        [TestCase(2, "Another Good Tale")]
        [TestCase(3, "Bambi")]
        public void CanGetDvdById(int id, string expectedTitle)
        {
            var repo = new MockRepository();
            var actual = repo.GetDvdById(id);

            Assert.AreEqual(expectedTitle, actual.Title);
        }

        [TestCase("Another Good Tale", 2)]
        [TestCase("Bambi", 3)]
        public void CanGetDvdsByTitle(string title, int id)
        {
            var repo = new MockRepository();
            var actual = repo.GetDvdsByTitle(title);

            Assert.AreEqual(id, actual[0].Id);
        }

        [TestCase("John Summers", "A Good Tale")]
        [TestCase("Howard Stern", "Bambi")]
        public void CanGetDvdsByDirector(string director, string expectedTitle)
        {
            var repo = new MockRepository();
            var actual = repo.GetDvdsByDirector(director);
            
            Assert.AreEqual(expectedTitle, actual[0].Title);
        }
    }
}
