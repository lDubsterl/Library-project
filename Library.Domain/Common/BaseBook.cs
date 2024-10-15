using Library.Domain.Entities;

namespace Library.Domain.Common
{
	public abstract class BaseBook
	{
		public required string ISBN { get; set; }
		public required string Title { get; set; }
		public required string Genre { get; set; }
		public required string Description { get; set; }
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
