using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
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
