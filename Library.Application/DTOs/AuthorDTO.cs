using Library.Application.Common.Mappings;
using Library.Domain.Common;
using Library.Domain.Entities;

namespace Library.Application.DTOs
{
	public class AuthorDTO : BaseAuthor, IMapFrom<Author>, IMapTo<Author>
	{
		public override bool Equals(object obj)
		{
			if (obj is null || obj is not AuthorDTO author)
				return false;
			if (author.Name == Name && author.Surname == Surname && author.Country == Country && author.BirthDate.Equals(BirthDate))
				return true;
			return false;
		}
		public override int GetHashCode()
		{
			return (Name, Surname, Country, BirthDate).GetHashCode();
		}
	}
}
