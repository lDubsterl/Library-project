using Library.Domain.Interfaces;

namespace Library.Domain.Entities
{
	public class UserBook: IEntity
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
		public int BookId { get; set; }
		public Book Book { get; set; }
		public DateOnly IssueDate { get; set; }
		public DateOnly RefundDate { get; set; }
	}
}
