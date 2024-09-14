using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.BaseEntities
{
	public abstract class BaseAuthor
	{
		public required string Name { get; set; }
		public required string Surname { get; set; }
		public required DateOnly BirthDate { get; set; }
		public required string Country { get; set; }
	}
}
