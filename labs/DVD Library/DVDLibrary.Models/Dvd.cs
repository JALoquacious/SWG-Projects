using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDLibrary.Models
{
    [Table("Dvd")]
    public class Dvd
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Rating { get; set; }
        public string Notes { get; set; }
    }
}
