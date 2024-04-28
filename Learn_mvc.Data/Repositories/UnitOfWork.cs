using Learn_mvc.Data.Database;
using Learn_mvc.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_mvc.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDBEntities _db;

        public UnitOfWork()
        {
            _db = new MyDBEntities();
        }

        private ICustomerRepository _Customers;
        public ICustomerRepository Customers
        {
            get
            {
                if (this._Customers == null)
                {
                    this._Customers = new CustomerRepository(_db);
                }
                return this._Customers;
            }
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
