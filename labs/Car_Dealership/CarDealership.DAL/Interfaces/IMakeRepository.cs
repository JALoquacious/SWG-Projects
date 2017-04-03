using CarDealership.Models.Tables;
using System.Collections.Generic;

namespace CarDealership.DAL.Interfaces
{
    public interface IMakeRepository
    {
        Make GetById(int makeId);
        List<Make> GetAll();
        void Insert(Make make);
    }
}
