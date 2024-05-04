using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn_mvc.Models
{
    public class IdentityLoginModel
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }

    public class IdentityRegisterModel
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}