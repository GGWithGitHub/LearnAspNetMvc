
using Learn_mvc.Data.Repository;
using Learn_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn_mvc.Logic
{
    public class RepoPatternViewRepository
    {
		public List<CustomerViewModel> GetCustomers()
		{
			var models = new List<CustomerViewModel>();
			var repoPatternRepository = new RepoPatternRepository();

			var lst_customer = repoPatternRepository.GetCustomers();

			foreach (var data in lst_customer)
			{
				models.Add(
					new CustomerViewModel()
					{
						CustomerId = data.customer_id,
						CompanyName = data.company_name,
						ContactName = data.contact_name,
						Address = data.address,
						City = data.city,
						State = data.state,
						Zip = data.zip,
						Country = data.country
					}
				);
			}

			return models;
		}
	}
}