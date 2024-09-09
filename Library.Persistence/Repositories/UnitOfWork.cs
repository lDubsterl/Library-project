using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces;
using Library.Persistence.Contexts;
using System.Collections;

namespace Library.Persistence.Repositories
{
	internal class UnitOfWork : IUnitOfWork
	{
		private LibraryDbContext _context;
		private Hashtable _repositories;
		public UnitOfWork(LibraryDbContext context)
		{
			_context = context;
			if (_repositories == null)
				_repositories = new Hashtable();
		}

		public IRepository<T> Repository<T>() where T : class, IEntity
		{

			var type = typeof(T).Name;

			if (!_repositories.ContainsKey(type))
			{
				var repositoryInstance = Activator.CreateInstance(typeof(GenericRepository<>).MakeGenericType(typeof(T)), _context);
				_repositories[type] = repositoryInstance;
			}
			return (IRepository<T>)_repositories[type]!;
		}
		public async Task<int> Save()
		{
			return await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
