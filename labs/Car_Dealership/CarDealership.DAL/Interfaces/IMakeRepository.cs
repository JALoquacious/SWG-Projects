using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.DAL.Interfaces
{
    public interface IMakeRepository
    {
        Make GetById(int makeId);
        List<Make> GetAll();
        void Insert(Make make);
    }
}
