using Library.Domain.Entities;

namespace Library.Domain.Common
{
	public abstract class BaseBook
	{
		public string ISBN { get; set; }
		public string Title { get; set; }
		public string Genre { get; set; }
		public string Description { get; set; }
		public bool IsOccupied { get; set; } = false;
		public int AuthorId {  get; set; }
		public override bool Equals(object obj)
		{
			if (obj is not BaseBook otherBook) return false;

			return ISBN == otherBook.ISBN;
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(ISBN);
		}
	}
}
