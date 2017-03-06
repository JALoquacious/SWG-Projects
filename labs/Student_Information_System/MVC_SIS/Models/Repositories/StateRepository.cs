using Exercises.Models.Data;
using System.Collections.Generic;
using System.Linq;

namespace Exercises.Models.Repositories
{
    public class StateRepository
    {
        private static List<State> _states;

        static StateRepository()
        {
            // sample data
            _states = new List<State>
            {
                new State { StateAbbreviation="KY", StateName="Kentucky" },
                new State { StateAbbreviation="MN", StateName="Minnesota" },
                new State { StateAbbreviation="OH", StateName="Ohio" },
            };
        }

        public static IEnumerable<State> GetAll()
        {
            return _states;
        }

        public static State Get(string stateAbbreviation)
        {
            return _states.FirstOrDefault(s => s.StateAbbreviation == stateAbbreviation);
        }

        public static void Add(State state)
        {
            _states.Add(state);
        }

        public static void Edit(State state)
        {
            var selectedState = _states.FirstOrDefault(s => s.StateAbbreviation == state.StateAbbreviation);

            selectedState.StateName = state.StateName;
            selectedState.StateAbbreviation = state.StateAbbreviation;
        }

        public static void Delete(string stateAbbreviation)
        {
            _states.RemoveAll(s => string.Compare(s.StateAbbreviation.ToUpper(), stateAbbreviation.ToUpper(), false) == 0);
        }
    }
}