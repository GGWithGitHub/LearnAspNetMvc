using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Models
{
    public class TestModel
    {
        [Remote("IsEmailAlready", "Test", ErrorMessage = "EmailId already exists.")]
        public string EmailId { get; set; }
    }
}