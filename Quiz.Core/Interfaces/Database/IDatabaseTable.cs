using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Interfaces.Database
{
    public interface IDatabaseTable<T> where T : class, new()
    {
        Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<int> CountAsync(CancellationToken cancellationToken = default);

        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

        Task CommitAsync(CancellationToken cancellationToken = default);

        IQueryable<T> Include(Expression<Func<T, object>> include);

        IQueryable<T> Includes(params Expression<Func<T, object>>[] includes);

        IEnumerable<T> GetAll();

        int Count();

        T GetFirst(Expression<Func<T, bool>> predicate);

        T GetFirst(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        T GetSingle(Expression<Func<T, bool>> predicate);

        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void DeleteWhere(Expression<Func<T, bool>> predicate);

        IQueryable<T> Queryable();

        void Commit();

        IEnumerable<T> BulkDelete(IEnumerable<T> entities);

        IEnumerable<T> BulkDeleteWhere(Expression<Func<T, bool>> predicate);

        IEnumerable<T> BulkAdd(IEnumerable<T> entities);
    }
}
