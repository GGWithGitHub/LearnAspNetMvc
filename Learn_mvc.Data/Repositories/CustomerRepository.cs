using Learn_mvc.Core.Models;
using Learn_mvc.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_mvc.Data.Repository
{
    public class CustomerRepository : Repository<tbl_customer>, ICustomerRepository
    {
        public MyDBEntities context
        {
            get
            {
                return db as MyDBEntities;
            }
        }

        public CustomerRepository(MyDBEntities _db) : base(_db)
        {

        }

        public List<CustomerModel> GetAllCustomers()
        {
            var customersData = GetAll();
            var customerModelList = new List<CustomerModel>();
            if (customersData.Any())
            {
                foreach (var data in customersData)
                {
                    customerModelList.Add(
                        new CustomerModel()
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
            }
            return customerModelList;
        }

        public CustomerModel GetCustomer(int id)
        {
            var customerData = Get(id);
            if (customerData != null)
            {
                return new CustomerModel()
                {
                    CustomerId = customerData.customer_id,
                    CompanyName = customerData.company_name,
                    ContactName = customerData.contact_name,
                    Address = customerData.address,
                    City = customerData.city,
                    State = customerData.state,
                    Zip = customerData.zip,
                    Country = customerData.country
                };
            }
            return null;
        }

        public bool AddCustomer(CustomerModel model)
        {
            tbl_customer tbl_Customer = new tbl_customer() { 
                customer_id = model.CustomerId,
                company_name = model.CompanyName,
                contact_name = model.ContactName,
                address = model.Address,
                city = model.City,
                state = model.State,
                zip = model.Zip,
                country = model.Country
            };
            Add(tbl_Customer);
            return true;
        }

        public bool UpdateCustomer(CustomerModel model)
        {
            tbl_customer tbl_Customer = new tbl_customer()
            {
                customer_id = model.CustomerId,
                company_name = model.CompanyName,
                contact_name = model.ContactName,
                address = model.Address,
                city = model.City,
                state = model.State,
                zip = model.Zip,
                country = model.Country
            };
            Update(tbl_Customer);
            return true;
        }

        public bool DeleteCustomer(int id)
        {
            Remove(id);
            return true;
        }
    }
}
