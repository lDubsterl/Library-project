using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces;
using Library.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories
{
	public class GenericRepository<T> : IRepository<T> where T : class, IEntity
	{
		private readonly LibraryDbContext _dbContext;
		public IQueryable<T> Entities => _dbContext.Set<T>();

		public GenericRepository(LibraryDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public T Add(T entity)
		{
			_dbContext.Set<T>().Add(entity);
			return entity;
		}

		public Task DeleteAsync(T entity)
		{
			T? exists = _dbContext.Set<T>().Find(entity.Id);
			if (exists != null)
				_dbContext.Entry(exists).State = EntityState.Detached;
			_dbContext.Set<T>().Remove(entity);
			return Task.CompletedTask;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public Task UpdateAsync(T entity)
		{
			T? exists = _dbContext.Set<T>().Find(entity.Id);
			if (exists != null)
				_dbContext.Entry(exists).CurrentValues.SetValues(entity);
			return Task.CompletedTask;
		}
	}
}
