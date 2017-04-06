using System;
using System.Collections.Generic;
using CarDealership.DAL.Interfaces;
using CarDealership.Models.Tables;
using System.Linq;

namespace CarDealership.DAL.Factories
{
    public class SpecialRepositoryMock : ISpecialRepository
    {
        private static List<Special> _specials;

        static SpecialRepositoryMock()
        {
            _specials = new List<Special>()
            {
                new Special()
                {
                    SpecialId = 1,
                    Name = "Free cup o' joe",
                    Description = "Get a steaming hot cup of coffee with the purchase of a new car."
                },
                new Special()
                {
                    SpecialId = 2,
                    Name = "Green shirt special",
                    Description = "You've got the luck of a leprechaun! $100 off for wearing a green shirt."
                },
                new Special()
                {
                    SpecialId = 3,
                    Name = "No shirt special",
                    Description = "Other dealers require a shirt. Not us. $200 off your purchase for being brave."
                },
                new Special()
                {
                    SpecialId = 4,
                    Name = "Recycling special",
                    Description = "Bring in your recycling and get $25 off of a recycled car."
                }
            };
        }

        public void Delete(int targetId)
        {
            _specials.RemoveAll(v => v.SpecialId == targetId);
        }

        public IEnumerable<Special> GetAll()
        {
            return _specials;
        }

        public Special GetById(int targetId)
        {
            return _specials.FirstOrDefault(s => s.SpecialId == targetId);
        }

        public void Insert(Special targetSpecial)
        {
            if (_specials.Any())
            {
                targetSpecial.SpecialId = _specials.Max(s => s.SpecialId) + 1;
            }
            else
            {
                targetSpecial.SpecialId = 1;
            }
            _specials.Add(targetSpecial);
        }
    }
}