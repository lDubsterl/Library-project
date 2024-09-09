using Library.Domain.Interfaces;

namespace Library.Application.Interfaces.Repositories
{
	public interface IUnitOfWork: IDisposable
	{
		public IRepository<T> Repository<T>() where T: class, IEntity;
		public Task<int> Save();
	}
}
