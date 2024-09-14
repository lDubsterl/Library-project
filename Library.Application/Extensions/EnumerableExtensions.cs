using Library.Shared.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Extensions
{
	public static class EnumerableExtensions
	{
		public static PaginatedResult<T> ToPaginatedList<T>(this IEnumerable<T> source, int pageNumber = 1, int pageSize = 10) where T : class
		{
			int count = source.Count();
			pageNumber = pageNumber <= 0 ? 1 : pageNumber;
			List<T> items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
			return PaginatedResult<T>.Create(items, count, pageNumber, pageSize);
		}
	}
}
