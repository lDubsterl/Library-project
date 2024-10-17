using Library.Domain.Common;
using Library.Domain.Interfaces;

namespace Library.Domain.Entities
{
	public class Author: BaseAuthor, IEntity
	{
		public List<Book> Books { get; set; }
	}
}
