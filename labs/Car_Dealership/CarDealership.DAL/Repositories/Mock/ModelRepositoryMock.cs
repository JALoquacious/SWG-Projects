using CarDealership.DAL.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDealership.DAL.Repositories.Mock
{
    public class ModelRepositoryMock : IModelRepository
    {
        private static List<Model> _models;

        static ModelRepositoryMock()
        {
            _models = new List<Model>()
            {
                new Model()
                {
                    ModelId = 1,
                    MakeId = 1,
                    Name = "Accord",
                    DateAdded = DateTime.Parse("1/1/2001")
                },
                new Model()
                {
                    ModelId = 2,
                    MakeId = 1,
                    Name = "Pilot",
                    DateAdded = DateTime.Parse("2/2/2002")
                },
                new Model()
                {
                    ModelId = 3,
                    MakeId = 2,
                    Name = "Solara",
                    DateAdded = DateTime.Parse("3/3/2003")
                },
                new Model()
                {
                    ModelId = 4,
                    MakeId = 3,
                    Name = "Century",
                    DateAdded = DateTime.Parse("4/4/2004")
                },
                new Model()
                {
                    ModelId = 5,
                    MakeId = 4,
                    Name = "Fiesta",
                    DateAdded = DateTime.Parse("5/5/2005")
                }
            };
        }

        public IEnumerable<Model> GetAll()
        {
            return _models;
        }

        public Model GetById(int targetId)
        {
            return _models.FirstOrDefault(m => m.ModelId == targetId);
        }

        public IEnumerable<Model> GetByMakeId(int makeId)
        {
            return _models.Where(m => m.MakeId == makeId);
        }

        public void Insert(Model targetModel)
        {
            if (_models.Any())
            {
                targetModel.ModelId = _models.Max(m => m.ModelId) + 1;
            }
            else
            {
                targetModel.ModelId = 1;
            }
            _models.Add(targetModel);
        }
    }
}
