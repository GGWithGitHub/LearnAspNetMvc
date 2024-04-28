using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_mvc.Core.Models
{
    public class CustomerModel
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
