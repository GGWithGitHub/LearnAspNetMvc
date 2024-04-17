using Learn_mvc.Database;
using Learn_mvc.Db_context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn_mvc.Repositories
{
    public class TestRepository
    {
        //BikeStoresEntities entities = new BikeStoresEntities();
        BikeStoresEntities entities;

        public TestRepository()
        {
            entities = new BikeStoresEntities();
        }
        public List<product> Get_products()
        {
            //using (var db = new BikeStoresEntities())
            //{
            //    return db.products.ToList();
            //}

            //try
            //{
            //    return entities.products.ToList();
            //}
            //finally
            //{
            //    entities.Dispose();
            //}

            return entities.products.ToList();
        }

        public void Dispose()
        {
            entities.Dispose();
        }

    }
}