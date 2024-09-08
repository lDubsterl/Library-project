using Library.Domain.Interfaces;

namespace Library.Domain.Entities
{
	public class Author: IEntity
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string Surname { get; set; }
		public required DateOnly BirthDate { get; set; }
		public required string Country { get; set; }
		public List<Book> Books { get; set; } = [];
	}
}
