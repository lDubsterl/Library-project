using Library.Shared.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Extensions
{
	public static class QueryableExtensions
	{
		public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber = 1, int pageSize = 10) where T : class
		{
			int count = await source.CountAsync();
			pageNumber = pageNumber <= 0 ? 1 : pageNumber;
			List<T> items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
			return PaginatedResult<T>.Create(items, count, pageNumber, pageSize);
		}
	}
}
