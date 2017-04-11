using CarDealership.DAL.Interfaces;
using CarDealership.Models.Tables;
using System.Collections.Generic;

namespace CarDealership.DAL.Repositories.Mock
{
    public class StateRepositoryMock : IStateRepository
    {
        private static List<State> _states;

        static StateRepositoryMock()
        {
            _states = new List<State>()
            {
                new State()
                {
                    StateId = "OH",
                    Name = "Ohio"
                },
                new State()
                {
                    StateId = "KY",
                    Name = "Kentucky"
                },
                new State()
                {
                    StateId = "MN",
                    Name = "Minnesota"
                },
                new State()
                {
                    StateId = "MI",
                    Name = "Michigan"
                }
            };
        }
        public IEnumerable<State> GetAll()
        {
            return _states;
        }
    }
}