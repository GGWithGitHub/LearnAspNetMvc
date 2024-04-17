using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Models
{
    public class Countries
    {
        [Required(ErrorMessage = "Please select at least one option")]
        public int? Id { get; set; }
        public string Name { get; set; }

    }
}