using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories
{
	public interface IBooksRepository
	{
		public Task<Book?> GetBookByISBN(string ISBN);
	}
}
