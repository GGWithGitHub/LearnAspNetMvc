using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Learn_mvc.Models
{
    public class College
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}