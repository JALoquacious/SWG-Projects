using System.ComponentModel.DataAnnotations;

namespace Exercises.Models.Data
{
    public class Course
    {
        public int CourseId { get; set; }

        [StringLength(25, MinimumLength = 3, ErrorMessage = "Course name length must be between 3 and 25.")]
        public string CourseName { get; set; }
    }
}