using CarDealership.DAL.Interfaces;
using CarDealership.Models.Tables;
using System.Collections.Generic;

namespace CarDealership.DAL.Repositories.Mock
{
    public class ColorRepositoryMock : IColorRepository
    {
        private static List<InteriorColor> _interiorColors;
        private static List<ExteriorColor> _exteriorColors;

        static ColorRepositoryMock()
        {
            _interiorColors = new List<InteriorColor>()
            {
                new InteriorColor()
                {
                    InteriorColorId = 1,
                    Name = "White"
                },
                new InteriorColor()
                {
                    InteriorColorId = 2,
                    Name = "Black"
                },
                new InteriorColor()
                {
                    InteriorColorId = 3,
                    Name = "Tan"
                },
                new InteriorColor()
                {
                    InteriorColorId = 4,
                    Name = "Gray"
                },
                new InteriorColor()
                {
                    InteriorColorId = 5,
                    Name = "Brown"
                },
            };

            _exteriorColors = new List<ExteriorColor>()
            {
                new ExteriorColor()
                {
                    ExteriorColorId = 1,
                    Name = "White"
                },
                new ExteriorColor()
                {
                    ExteriorColorId = 2,
                    Name = "Black"
                },
                new ExteriorColor()
                {
                    ExteriorColorId = 3,
                    Name = "Red"
                },
                new ExteriorColor()
                {
                    ExteriorColorId = 4,
                    Name = "Blue"
                },
                new ExteriorColor()
                {
                    ExteriorColorId = 5,
                    Name = "Yellow"
                },
            };
        }

        public IEnumerable<ExteriorColor> GetAllExterior()
        {
            return _exteriorColors;
        }

        public IEnumerable<InteriorColor> GetAllInterior()
        {
            return _interiorColors;
        }
    }
}