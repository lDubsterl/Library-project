
using Library.Domain.BaseEntities;
using Library.Domain.Interfaces;

namespace Library.Domain.Entities
{
    public class Book: BaseBook, IEntity
    {
        public int Id { get; set; }
		public int AuthorId { get; set; }
		public required Author Author { get; set; }
    }
}
