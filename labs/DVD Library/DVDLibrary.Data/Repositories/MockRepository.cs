using DVDLibrary.Data.Interfaces;
using DVDLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace DVDLibrary.Data.Repositories
{
    public class MockRepository : IDvdRepository
    {
        private static List<Dvd> _dvds;

        static MockRepository()
        {
            _dvds = new List<Dvd>()
            {
                new Dvd
                {
                    Id = 1,
                    Title = "A Good Tale",
                    ReleaseYear = 1999,
                    Director = "John Summers",
                    Rating = "R",
                    Notes = "A mighty good one if you ask me"
                },
                new Dvd
                {
                    Id = 2,
                    Title = "Another Good Tale",
                    ReleaseYear = 2010,
                    Director = "John Summers",
                    Rating = "G",
                    Notes = "I liked this one"
                },
                new Dvd
                {
                    Id = 3,
                    Title = "Bambi",
                    ReleaseYear = 1980,
                    Director = "Howard Stern",
                    Rating = "NC-17",
                    Notes = "A real terror"
                }
            };
        }

        public void AddDvd(Dvd createdDvd)
        {
            if (_dvds.Any())
            {
                createdDvd.Id = _dvds.Max(d => d.Id) + 1;
            }
            else
            {
                createdDvd.Id = 1;
            }
            _dvds.Add(createdDvd);
        }

        public void DeleteDvd(int targetId)
        {
            _dvds.RemoveAll(d => d.Id == targetId);
        }

        public List<Dvd> GetAllDvds()
        {
            return _dvds;
        }

        public Dvd GetDvdById(int targetId)
        {
            return _dvds.FirstOrDefault(d => d.Id == targetId);
        }

        public List<Dvd> GetDvdsByDirector(string director)
        {
            return _dvds.Where(d => d.Director == director).ToList();
        }

        public List<Dvd> GetDvdsByRating(string rating)
        {
            return _dvds.Where(d => d.Rating == rating).ToList();
        }

        public List<Dvd> GetDvdsByReleaseYear(int releaseYear)
        {
            return _dvds.Where(d => d.ReleaseYear == releaseYear).ToList();
        }

        public List<Dvd> GetDvdsByTitle(string title)
        {
            return _dvds.Where(d => d.Title == title).ToList();
        }

        public void UpdateDvd(Dvd updatedDvd)
        {
            _dvds.RemoveAll(d => d.Id == updatedDvd.Id);
            _dvds.Add(updatedDvd);
        }
    }
}
