using Learn_mvc.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_mvc.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }

        int SaveChanges();
    }
}
