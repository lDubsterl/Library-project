﻿using Library.Domain.Interfaces;
using Fluent.Infrastructure.FluentModel;
using System.Data.Entity;

namespace Library.Domain.Repositories
{
	internal class GenericRepository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _dbContext;
		public IQueryable<T> Entities => _dbContext.Set<T>();

		public GenericRepository(ApplicationDbContext dbContext)
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
			_dbContext.Set<T>().Remove(entity);
			return Task.CompletedTask;
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public Task UpdateAsync(T entity)
		{
			T exists = _dbContext.Set<T>().Find(entity);
			_dbContext.Entry(exists).CurrentValues.SetValues(entity);
			return Task.CompletedTask;
		}
	}
}
