using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABPCinemaAPI.DAL.Persistence;
using ABPCinemaAPI.DAL.Repositories.Contracts;
using ABPCinemaAPI.DAL.Specification;
using ABPCinemaAPI.DAL.Specification.Evaluator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ABPCinemaAPI.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly DbSet<T> _dbSet;
        private readonly AppDbContext _dbContext;

        protected GenericRepository(AppDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public T Create(T entity)
        {
            return _dbContext.Set<T>().Add(entity).Entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var tmp = await _dbContext.Set<T>().AddAsync(entity);
            return tmp.Entity;
        }

        public Task CreateRangeAsync(IEnumerable<T> items)
        {
            return _dbContext.Set<T>().AddRangeAsync(items);
        }

        public EntityEntry<T> Update(T entity)
        {
            return _dbContext.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> items)
        {
            _dbContext.Set<T>().UpdateRange(items);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> items)
        {
            _dbContext.Set<T>().RemoveRange(items);
        }

        public void Attach(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
        }

        public EntityEntry<T> Entry(T entity)
        {
            return _dbContext.Entry(entity);
        }

        public void Detach(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        public Task ExecuteSqlRaw(string query)
        {
            return _dbContext.Database.ExecuteSqlRawAsync(query);
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(IBaseSpecification<T, TResult> specification)
        {
            return await ApplySpecificationForList(specification).ToListAsync();
        }

        public async Task<TResult> GetFirstOrDefaultAsync<TResult>(IBaseSpecification<T, TResult> specification)
        {
            return await ApplySpecificationForList(specification).FirstOrDefaultAsync();
        }

        private IQueryable<TResult> ApplySpecificationForList<TResult>(IBaseSpecification<T, TResult> specification)
        {
            return SpecificationEvaluator<T, TResult>.GetQuery(_dbSet.AsQueryable(), specification);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}