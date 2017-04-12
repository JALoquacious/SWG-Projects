using CarDealership.Models.Tables;
using System.Collections.Generic;
using CarDealership.Models.Queries;

namespace CarDealership.DAL.Interfaces
{
    public interface IModelRepository
    {
        Model GetById(int modelId);
        IEnumerable<Model> GetAll();
        IEnumerable<Model> GetByMakeId(int makeId);
        IEnumerable<ModelUserQueryRow> GetModelUserTable();
        void Insert(Model model);
    }
}
