using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Learn_mvc.Models
{
    public class Student2
    {
		public int Std_id { get; set; }

		[Required]
		public string Std_name { get; set; }

		[Required]
		public string Std_rollnumber { get; set; }

		[Required]
		public string Std_class { get; set; }

		[Required]
		public string Std_subject { get; set; }
	}
}