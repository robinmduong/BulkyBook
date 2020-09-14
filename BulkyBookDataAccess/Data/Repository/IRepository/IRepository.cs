using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BulkyBookDataAccess.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);

        IEnumerable<T> GetAll(
            //The Func has 2 params, and output is a bool
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
        );

        T GetFirstOrdefault(
            Expression<Func<T, bool>> filter = null,
            //No need for orderedBy because it'll retrieve 1 credit card only
            string includeProperties = null
        );

        void Add(T entity);
        void Remove(int id);
        void Remove(T entity);
        void RemoveRagne(IEnumerable<T> entity); //remove complete range of entities
    }
}
