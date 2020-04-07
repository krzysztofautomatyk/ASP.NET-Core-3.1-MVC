using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Ksiegarnia.DataAccess.Repository.IRepository
{
    /// <summary>
    /// Interfejs odpowiedzalny za komunikację z bazą danych
    /// </summary>
    /// <typeparam name="T">Model(klasa)</typeparam>
    public interface IRepository<T> where T:class
    {
        T Get(int id);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );

        void Add(T entity);

        void Remove(int id);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);

    }
}
