using Learn_mvc.Core.Models;
using Learn_mvc.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_mvc.Data.Repository
{
    public interface ICustomerRepository: IRepository<tbl_customer>
    {
        List<CustomerModel> GetAllCustomers();
        CustomerModel GetCustomer(int id);
        bool AddCustomer(CustomerModel model);
        bool UpdateCustomer(CustomerModel model);
        bool DeleteCustomer(int id);
    }
}
