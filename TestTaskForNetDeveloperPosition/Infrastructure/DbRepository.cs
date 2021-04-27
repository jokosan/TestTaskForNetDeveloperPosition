using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using TestTaskForNetDeveloperPosition.Infrastructure.Contract;

namespace TestTaskForNetDeveloperPosition.Infrastructure
{
    public class DbRepository<T> : IGetRepository<T>, IDefaultRepository<T>, IGetInclude<T> where T : class
    {
        internal TestTaskForNetDeveloperPositionEntities _testTaskForNetDeveloperPositionContext;
        internal DbSet<T> DbSeT;

        public DbRepository(TestTaskForNetDeveloperPositionEntities entities)
        {
            _testTaskForNetDeveloperPositionContext = entities;
            DbSeT = entities.Set<T>();
        }

        #region IGetRepository
        public IEnumerable<T> Get()
            =>  DbSeT.AsEnumerable<T>().AsQueryable().AsNoTracking();

        public T GetById(int? id)
            =>  DbSeT.Find(id);

        #endregion

        #region IDefaultRepository
        public virtual void Insert(T entity)
        {
        //    if (_testTaskForNetDeveloperPositionContext.Entry(entity).State == EntityState.Modified)
                DbSeT.Add(entity);
        }

        public void Insert(List<T> entity)
        {
            {
                DbSeT.AddRange(entity);
            }
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = DbSeT.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_testTaskForNetDeveloperPositionContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSeT.Attach(entityToDelete);
            }

            DbSeT.Remove(entityToDelete);
        }
        public virtual void Update(T entityToUpdate)
        {
            _testTaskForNetDeveloperPositionContext.Set<T>().AddOrUpdate(entityToUpdate);
        }
        #endregion

        #region IGetInclude
        public IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter)
            =>  DbSeT.Where(filter).AsNoTracking();

        public IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter, string children)
            => DbSeT.Include(children).Where(filter).AsNoTracking();

        public IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter, string childrenOne, string childrenTwo)
            =>  DbSeT.Include(childrenOne).Include(childrenTwo).Where(filter).AsNoTracking();

        public IEnumerable<T> GetInclude(string children)
            =>  DbSeT.Include(children).AsNoTracking().AsEnumerable<T>().AsQueryable();
        #endregion
             
    }
}