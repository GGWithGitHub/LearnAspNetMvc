using Learn_mvc.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_mvc.Data.Repository
{
	public class RepoPatternRepository
	{
		public List<tbl_customer> GetCustomers()
		{
			using (var worker = new DBWorker())
			{
				return worker.TblCustomerEntity.Get().ToList();
			}
		}
	}
}
