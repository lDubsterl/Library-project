using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces
{
	internal interface IRepository<T> where T : class
	{
		public IQueryable<T> Entities { get; }
		public Task<T> GetByIdAsync(int id);
		public Task<List<T>> GetAllAsync();
		public T Add(T entity);
		public Task UpdateAsync(T entity);
		public Task DeleteAsync(T entity);
	}
}
