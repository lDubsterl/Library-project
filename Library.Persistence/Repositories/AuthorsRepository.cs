using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Library.Domain.Interfaces;

namespace Library.Persistence.Repositories
{
	internal class AuthorsRepository
	{
		readonly IRepository<Author> _repository;
		public AuthorsRepository(IRepository<Author> repository)
		{
			_repository = repository;
		}
		public async Task<List<Book>?> GetBooksByAuthor(int authorId)
		{
			var author = await _repository.Entities.Where(a => a.Id == authorId).FirstOrDefaultAsync();
			return author?.Books;
		}
	}
}
