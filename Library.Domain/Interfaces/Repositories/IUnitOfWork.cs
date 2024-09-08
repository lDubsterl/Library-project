namespace Library.Domain.Interfaces
{
	public interface IUnitOfWork: IDisposable
	{
		public IRepository<T> Repository<T>() where T: class, IEntity;
		public Task<int> Save();
	}
}
