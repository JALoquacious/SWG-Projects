using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealership.Models.Tables
{
    public class Make
    {
        public Make()
        {
            DateAdded = DateTime.Now;
        }

        [Column(TypeName = "DATETIME2")]
        public DateTime DateAdded { get; set; }
        public int MakeId  { get; set; }
        public string Name { get; set; }
    }
}
