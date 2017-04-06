using CarDealership.Models.Tables;

namespace CarDealership.DAL.Interfaces
{
    public interface IContactRepository
    {
        void Insert(Contact contact);
    }
}
