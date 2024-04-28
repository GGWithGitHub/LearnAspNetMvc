using Learn_mvc.Data.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Learn_mvc.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly MyDBEntities db;

        public Repository(MyDBEntities _db)
        {
            db = _db;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return db.Set<TEntity>().ToList();
        }

        public IEnumerable Find(Expression<Func<TEntity, bool>> predicate)
        {
            return db.Set<TEntity>().Where(predicate);
        }

        public TEntity Get(object Id)
        {
            return db.Set<TEntity>().Find(Id);
        }

        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            db.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            db.Set<TEntity>().RemoveRange(entities);
        }

        public void Remove(object Id)
        {
            TEntity entity = db.Set<TEntity>().Find(Id);
            this.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
