using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealership.Models.Tables
{
    public class Model
    {
        public Model()
        {
            DateAdded = DateTime.Now;
        }

        [Column(TypeName = "DATETIME2")]
        public DateTime DateAdded { get; set; }
        public int ModelId        { get; set; }
        public int MakeId         { get; set; }
        public int Year           { get; set; }
        public string UserId      { get; set; }
        public string Name        { get; set; }
    }
}
