using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3,ErrorMessage="Length should be at least 3")]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

    }
}