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
                    StateId = 1,
                    Name = "OH"
                },
                new State()
                {
                    StateId = 2,
                    Name = "KY"
                },
                new State()
                {
                    StateId = 3,
                    Name = "MN"
                },
                new State()
                {
                    StateId = 4,
                    Name = "MI"
                }
            };
        }
        public IEnumerable<State> GetAll()
        {
            return _states;
        }
    }
}