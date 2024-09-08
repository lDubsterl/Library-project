using Library.Domain.Interfaces;

namespace Library.Application.Interfaces
{
	public interface IRepository<T> where T : class, IEntity
	{
		public IQueryable<T> Entities { get; }
		public Task<T?> GetByIdAsync(int id);
		public Task<List<T>> GetAllAsync();
		public T Add(T entity);
		public Task UpdateAsync(T entity);
		public Task DeleteAsync(T entity);
	}
}
