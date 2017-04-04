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
                    Name = "GT-R",
                    DateAdded = DateTime.Parse("1/1/2001")
                },
                new Model()
                {
                    ModelId = 2,
                    Name = "Pilot",
                    DateAdded = DateTime.Parse("2/2/2002")
                },
                new Model()
                {
                    ModelId = 3,
                    Name = "Solara",
                    DateAdded = DateTime.Parse("3/3/2003")
                },
                new Model()
                {
                    ModelId = 4,
                    Name = "Century",
                    DateAdded = DateTime.Parse("4/4/2004")
                },
                new Model()
                {
                    ModelId = 5,
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
