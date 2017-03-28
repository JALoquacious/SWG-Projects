using DVDLibrary.Models;
using System.Collections.Generic;

namespace DVDLibrary.Data.Interfaces
{
    public interface IDvdRepository
    {
        Dvd GetDvdById(int id);
        void AddDvd(Dvd dvd);
        void UpdateDvd(Dvd dvd);
        void DeleteDvd(int id);
        List<Dvd> GetAllDvds();
        List<Dvd> GetDvdsByTitle(string title);
        List<Dvd> GetDvdsByReleaseYear(int releaseYear);
        List<Dvd> GetDvdsByDirector(string director);
        List<Dvd> GetDvdsByRating(string rating);
    }
}
