namespace Library.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public required string ISBN { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public required string Description { get; set; }
		public int? AuthorId { get; set; }
		public Author? Author { get; set; }
		public DateTime TakeTime { get; set; }
        public DateTime RefundTime { get; set; }
    }
}
