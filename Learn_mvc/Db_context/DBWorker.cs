using Learn_mvc.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn_mvc.Db_context
{
    public class DBWorker : IDisposable
    {
        BikeStoresEntities bikeStoresEntities = null;
        public DBWorker()
        {
            bikeStoresEntities = new BikeStoresEntities();
        }
        public List<Employee> Get_list()
        {
            var list = new List<Employee>() { 
                new Employee { Id=1, Name="A", Email="A@gmail.com", Address="A address" },
                new Employee { Id=2, Name="B", Email="B@gmail.com", Address="B address" },
                new Employee { Id=3, Name="C", Email="C@gmail.com", Address="C address" },
                new Employee { Id=4, Name="D", Email="D@gmail.com", Address="D address" },
                new Employee { Id=5, Name="E", Email="E@gmail.com", Address="E address" },
                new Employee { Id=6, Name="F", Email="F@gmail.com", Address="F address" },
                new Employee { Id=7, Name="G", Email="G@gmail.com", Address="G address" },
                new Employee { Id=8, Name="H", Email="H@gmail.com", Address="H address" },
                new Employee { Id=9, Name="I", Email="I@gmail.com", Address="I address" },
                new Employee { Id=10, Name="J", Email="J@gmail.com", Address="J address" },
            };

            return list;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    bikeStoresEntities.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

    }
}