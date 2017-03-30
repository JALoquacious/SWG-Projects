using DVDLibrary.Data.Interfaces;
using DVDLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace DVDLibrary.Data.Repositories
{
    public class EFRepository : IDvdRepository
    {
        DvdLibraryEntities context;
        public EFRepository()
        {
            context = new DvdLibraryEntities();
        }

        public void AddDvd(Dvd dvd)
        {
            context.Dvds.Add(dvd);
            context.SaveChanges();
        }
        public void DeleteDvd(int id)
        {
            var dvd = context.Dvds.Find(id);
            if (dvd != null) context.Dvds.Remove(dvd);
            context.SaveChanges();
        }
        public List<Dvd> GetAllDvds()
        {
            return context.Dvds.Select(d => d).ToList();
        }
        public Dvd GetDvdById(int id)
        {
            return context.Dvds.Find(id);
        }
        public List<Dvd> GetDvdsByDirector(string director)
        {
            return context.Dvds.Where(d => d.Director == director).ToList();
        }
        public List<Dvd> GetDvdsByRating(string rating)
        {
            return context.Dvds.Where(d => d.Rating == rating).ToList();
        }
        public List<Dvd> GetDvdsByReleaseYear(int releaseYear)
        {
            return context.Dvds.Where(d => d.ReleaseYear == releaseYear).ToList();
        }
        public List<Dvd> GetDvdsByTitle(string title)
        {
            return context.Dvds.Where(d => d.Title == title).ToList();
        }
        public void UpdateDvd(Dvd dvd)
        {
            context.Entry(dvd).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
