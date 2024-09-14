using Library.Domain.BaseEntities;
using Library.Domain.Interfaces;

namespace Library.Domain.Entities
{
	public class Author: BaseAuthor, IEntity
	{
		public int Id { get; set; }
		public IEnumerable<Book> Books { get; set; } = [];
	}
}
