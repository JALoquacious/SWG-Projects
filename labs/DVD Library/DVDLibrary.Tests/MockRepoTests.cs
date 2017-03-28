using DVDLibrary.Data.Repositories;
using NUnit.Framework;
using System.Linq;

namespace DVDLibrary.Tests
{
    [TestFixture]
    public class MockRepoTests
    {
        [Test]
        public void MockCanLoadDvds()
        {
            var repo = new MockRepository();
            var dvds = repo.GetAllDvds();

            Assert.IsNotNull(dvds);
            Assert.AreEqual(3, dvds.ToList().Count());
        }

        [TestCase(1, "A Good Tale")]
        [TestCase(2, "Another Good Tale")]
        [TestCase(3, "Bambi")]
        public void MockCanGetDvdById(int id, string expectedTitle)
        {
            var repo = new MockRepository();
            var actual = repo.GetDvdById(id);

            Assert.AreEqual(expectedTitle, actual.Title);
        }

        [TestCase("A Good Tale", 1)]
        [TestCase("Another Good Tale", 2)]
        [TestCase("Bambi", 3)]
        public void MockCanGetDvdsByTitle(string title, int expectedId)
        {
            var repo = new MockRepository();
            var actual = repo.GetDvdsByTitle(title);

            Assert.AreEqual(expectedId, actual[0].Id);
        }

        [TestCase(1999, 1)]
        [TestCase(2010, 2)]
        [TestCase(1980, 3)]
        public void MockCanGetDvdsByReleaseYear(int releaseYear, int expectedId)
        {
            var repo = new MockRepository();
            var actual = repo.GetDvdsByReleaseYear(releaseYear);

            Assert.AreEqual(expectedId, actual[0].Id);
        }

        [TestCase("John Summers", "A Good Tale")]
        [TestCase("Howard Stern", "Bambi")]
        public void MockCanGetDvdsByDirector(string director, string expectedTitle)
        {
            var repo = new MockRepository();
            var actual = repo.GetDvdsByDirector(director);
            
            Assert.AreEqual(expectedTitle, actual[0].Title);
        }

        [TestCase("R", "A Good Tale")]
        [TestCase("G", "Another Good Tale")]
        [TestCase("NC-17", "Bambi")]
        public void MockCanGetDvdsByRating(string rating, string expectedTitle)
        {
            var repo = new MockRepository();
            var actual = repo.GetDvdsByRating(rating);

            Assert.AreEqual(expectedTitle, actual[0].Title);
        }
    }
}
