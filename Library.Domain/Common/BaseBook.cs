using Library.Domain.Entities;

namespace Library.Domain.Common
{
	public abstract class BaseBook
	{
		public required string ISBN { get; set; }
		public required string Title { get; set; }
		public required string Genre { get; set; }
		public required string Description { get; set; }
	}
}
