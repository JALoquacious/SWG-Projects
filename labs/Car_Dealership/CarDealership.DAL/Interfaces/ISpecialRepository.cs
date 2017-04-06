using CarDealership.Models.Tables;
using System.Collections.Generic;

namespace CarDealership.DAL.Interfaces
{
    public interface ISpecialRepository
    {
        Special GetById(int specialId);
        IEnumerable<Special> GetAll();
        void Insert(Special special);
        void Delete(int specialId);
    }
}
