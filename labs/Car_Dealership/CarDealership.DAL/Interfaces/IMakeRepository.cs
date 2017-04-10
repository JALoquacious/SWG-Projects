using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System.Collections.Generic;

namespace CarDealership.DAL.Interfaces
{
    public interface IMakeRepository
    {
        Make GetById(int makeId);
        IEnumerable<Make> GetAll();
        IEnumerable<MakeUserQueryRow> GetMakeUserTable();
        void Insert(Make make);
    }
}
