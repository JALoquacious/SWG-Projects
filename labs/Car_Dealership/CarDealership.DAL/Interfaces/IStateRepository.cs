using CarDealership.Models.Tables;
using System.Collections.Generic;

namespace CarDealership.DAL.Interfaces
{
    public interface IStateRepository
    {
        IEnumerable<State> GetAll();
    }
}
