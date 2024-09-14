using Library.Application.Common.Mappings;
using Library.Domain.BaseEntities;
using Library.Domain.Entities;

namespace Library.Application.DTOs
{
	public class AuthorDTO : BaseAuthor, IMapFrom<Author>
	{
	}
}
