﻿using Exercises.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exercises.Models.Data
{
    public class Student
    {
        public int StudentId { get; set; }

        [StringLength(20, ErrorMessage = "First name cannot exceed 20 characters.")]
        [Required(ErrorMessage = "Please enter a FIRST name.")]
        public string FirstName { get; set; }

        [StringLength(20, ErrorMessage = "Last name cannot exceed 20 characters.")]
        [Required(ErrorMessage = "Please enter a LAST name.")]
        public string LastName { get; set; }

        [GpaRange(ErrorMessage = "GPA must be between 0 and 4.")]
        public decimal GPA { get; set; }
        public Address Address { get; set; }
        public Major Major { get; set; }
        public List<Course> Courses { get; set; }
    }
}