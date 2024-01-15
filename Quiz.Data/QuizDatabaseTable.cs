using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Quiz.Core.Interfaces.Database;
using Quiz.Core.Models.Database;
using Quiz.Core.Interfaces.UserManagement;

namespace Quiz.Data
{
    public class QuizDatabaseTable<T> : IDatabaseTable<T> where T : class, new()
    {
        private readonly QuizContext _context;
        private readonly IClaimsService _claimsService;

        public QuizDatabaseTable(QuizContext context, IClaimsService claimsService)
        {
            _context = context;
            _claimsService = claimsService;
        }

        public IQueryable<T> Include(Expression<Func<T, object>> include)
        {
            return Queryable().Include(include);
        }

        public IQueryable<T> Includes(params Expression<Func<T, object>>[] includes)
        {
            var query = Queryable();
            return includes.Aggregate(query, (current, include) => current.Include(include));
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public virtual int Count()
        {
            return _context.Set<T>().Count();
        }

        public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.AsEnumerable();
        }

        public T GetFirst(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public T GetFirst(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.SingleOrDefault(predicate);
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            var dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            var dbEntityEntry = _context.Entry<T>(entity);

            if (entity as ISoftDelete == null)
                dbEntityEntry.State = EntityState.Deleted;
            else
            {
                dbEntityEntry.State = EntityState.Modified;
                ((ISoftDelete)entity).IsDeleted = true;
            }
        }

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = _context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                _context.Entry<T>(entity).State = EntityState.Deleted;
            }
        }

        public virtual IQueryable<T> Queryable()
        {
            var entities = _context.Set<T>();

            if (entities as IQueryable<ISoftDelete> == null)
                return entities;

            return entities.Where(t => ((ISoftDelete)t).IsDeleted == false);
        }

        public virtual void Commit()
        {
            AuditChanges();
            _context.SaveChanges();
        }

        public Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToArrayAsync(cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().CountAsync(cancellationToken);
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToArrayAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            return await includeProperties.Aggregate(query, (current, include) => current.Include(include))
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            return await includeProperties.Aggregate(query, (current, include) => current.Include(include))
                .Where(predicate)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken)
        {
            return await _context.Set<T>().Where(predicate).ToArrayAsync(cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = new CancellationToken())
        {
            await _context.AddAsync(entity, cancellationToken);
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken = new CancellationToken())
        {
            if (entity as ISoftDelete == null)
                _context.Remove(entity);
            else
                ((ISoftDelete)entity).IsDeleted = true;

            return Task.FromResult(0);
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            AuditChanges();
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToArrayAsync();
        }

        public async Task CommitAsync()
        {
            AuditChanges();
            await _context.SaveChangesAsync();
        }

        private void AuditChanges()
        {
            var userId = _claimsService.UserProfileId;
            var nowUtc = DateTime.UtcNow;

            foreach (var auditableEntity in _context.ChangeTracker.Entries<AuditableUtcEntity>())
            {
                if (auditableEntity.State == EntityState.Added)
                {
                    //only set if we did not specify
                    if (auditableEntity.Entity.CreatedBy == 0)
                        auditableEntity.Entity.CreatedBy = userId;

                    auditableEntity.Entity.CreatedOnUtc = nowUtc;
                    auditableEntity.Entity.UpdatedOnUtc = nowUtc;
                    auditableEntity.Entity.UpdatedBy = auditableEntity.Entity.CreatedBy;
                }

                if (auditableEntity.State == EntityState.Modified)
                {
                    auditableEntity.Entity.UpdatedBy = userId;
                    auditableEntity.Entity.UpdatedOnUtc = nowUtc;
                }
            }
        }

        public IEnumerable<T> BulkDelete(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return default;
        }

        public IEnumerable<T> BulkDeleteWhere(Expression<Func<T, bool>> predicate)
        {
            return BulkDelete(_context.Set<T>().Where(predicate));
        }

        public IEnumerable<T> BulkAdd(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return default;
        }
    }
}