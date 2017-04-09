using CarDealership.Models.Tables;
using System.Collections.Generic;

namespace CarDealership.DAL.Interfaces
{
    public interface IModelRepository
    {
        Model GetById(int modelId);
        IEnumerable<Model> GetAll();
        IEnumerable<Model> GetByMakeId(int makeId);
        void Insert(Model model);
    }
}
