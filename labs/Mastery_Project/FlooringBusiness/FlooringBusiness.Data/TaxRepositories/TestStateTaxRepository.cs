using System;
using System.Collections.Generic;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.Data.TaxRepositories
{
    public class TestStateTaxRepository : IStateTaxRepository
    {
        private static readonly List<StateTax> StateTaxes;

        static TestStateTaxRepository()
        {
            StateTaxes = new List<StateTax>();

            StateTax ny = new StateTax()
            {
                StateAbbreviation = "NY",
                StateName = "New York",
                TaxRate = 8.875m
            };

            StateTax az = new StateTax()
            {
                StateAbbreviation = "AZ",
                StateName = "Arizona",
                TaxRate = 5m
            };

            StateTax oh = new StateTax()
            {
                StateAbbreviation = "OH",
                StateName = "Ohio",
                TaxRate = 6.25m
            };

            StateTax ca = new StateTax()
            {
                StateAbbreviation = "CA",
                StateName = "California",
                TaxRate = 9.75m
            };

            StateTaxes.Add(ny);
            StateTaxes.Add(az);
            StateTaxes.Add(oh);
            StateTaxes.Add(ca);
        }

        public StateTax GetStateTax(string state)
        {
            return StateTaxes.Find(s =>
                (
                    s.StateAbbreviation == state.ToUpper() ||
                    String.Equals(s.StateName, state, StringComparison.CurrentCultureIgnoreCase)
                )
            );
        }
    }
}
