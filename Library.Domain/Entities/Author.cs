using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
	public class Author
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string Surname { get; set; }
		public required DateOnly BirthDate { get; set; }
		public required string Country { get; set; }
	}
}
