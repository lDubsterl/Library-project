namespace Library.Domain.Common
{
	public abstract class BaseAuthor
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string Surname { get; set; }
		public required DateOnly BirthDate { get; set; }
		public required string Country { get; set; }
	}
}
