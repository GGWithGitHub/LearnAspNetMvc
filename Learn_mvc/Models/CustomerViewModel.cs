﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn_mvc.Models
{
	public class CustomerViewModel
	{
		public int CustomerId { get; set; }
		public string CompanyName { get; set; }
		public string ContactName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string Country { get; set; }
	}
}