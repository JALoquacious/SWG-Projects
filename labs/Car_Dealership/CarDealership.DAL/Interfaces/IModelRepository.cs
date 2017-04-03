using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.DAL.Interfaces
{
    public interface IModelRepository
    {
        Model GetById(int makeId);
        List<Model> GetAll();
        void Insert(Model model);
    }
}
