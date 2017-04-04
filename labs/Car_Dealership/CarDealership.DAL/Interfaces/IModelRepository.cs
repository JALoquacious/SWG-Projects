using CarDealership.Models.Tables;
using System.Collections.Generic;

namespace CarDealership.DAL.Interfaces
{
    public interface IModelRepository
    {
        Model GetById(int makeId);
        IEnumerable<Model> GetAll();
        void Insert(Model model);
    }
}
