using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn_mvc.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string FatherName { get; set; }

        public string DOB { get; set; }

        public long MobileNo { get; set; }

        public int Age { get; set; }

        public string EmailId { get; set; }
    }
}