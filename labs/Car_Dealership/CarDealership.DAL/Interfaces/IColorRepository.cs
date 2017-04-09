using CarDealership.Models.Tables;
using System.Collections.Generic;

namespace CarDealership.DAL.Interfaces
{
    public interface IColorRepository
    {
        IEnumerable<InteriorColor> GetAllInterior();
        IEnumerable<ExteriorColor> GetAllExterior();
    }
}
