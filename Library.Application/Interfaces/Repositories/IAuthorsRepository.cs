using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories
{
	public interface IAuthorsRepository
	{
		public Task<List<Book>?> GetBooksByAuthor(int authorId);
	}
}
