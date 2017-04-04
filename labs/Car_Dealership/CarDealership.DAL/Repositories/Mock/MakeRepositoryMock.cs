using CarDealership.DAL.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDealership.DAL.Repositories.Mock
{
    public class MakeRepositoryMock : IMakeRepository
    {
        private static List<Make> _makes;

        static MakeRepositoryMock()
        {
            _makes = new List<Make>()
            {
                new Make()
                {
                    MakeId = 1,
                    Name = "Chevrolet",
                    DateAdded = DateTime.Parse("1/1/2001")
                },
                new Make()
                {
                    MakeId = 2,
                    Name = "Ford",
                    DateAdded = DateTime.Parse("2/2/2002")
                },
                new Make()
                {
                    MakeId = 3,
                    Name = "Honda",
                    DateAdded = DateTime.Parse("3/3/2003")
                },
                new Make()
                {
                    MakeId = 4,
                    Name = "Peugeot",
                    DateAdded = DateTime.Parse("4/4/2004")
                }
            };
        }

        public IEnumerable<Make> GetAll()
        {
            return _makes;
        }

        public Make GetById(int targetId)
        {
            return _makes.FirstOrDefault(m => m.MakeId == targetId);
        }

        public void Insert(Make targetMake)
        {
            if (_makes.Any())
            {
                targetMake.MakeId = _makes.Max(m => m.MakeId) + 1;
            }
            else
            {
                targetMake.MakeId = 1;
            }
            _makes.Add(targetMake);
        }
    }
}
