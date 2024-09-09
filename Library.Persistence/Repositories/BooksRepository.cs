using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories
{
	internal class BooksRepository
	{
		readonly IRepository<Book> _repository;
		public BooksRepository(IRepository<Book> repository)
		{
			_repository = repository;
		}
		public async Task<List<Book>> GetBookByISBN(string ISBN)
		{
			 return await _repository.Entities.Where(b => b.ISBN == ISBN).ToListAsync();
		}
	}
}
