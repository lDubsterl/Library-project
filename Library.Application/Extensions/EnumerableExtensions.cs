using Microsoft.AspNetCore.Mvc;

namespace Library.Application.Extensions
{
	public static class EnumerableExtensions
	{
		public static IActionResult ToPaginatedList<T>(this IEnumerable<T> source, int pageNumber = 1, int pageSize = 10) where T : class
		{
			int count = source.Count();
			pageNumber = pageNumber <= 0 ? 1 : pageNumber;
			List<T> items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
			return new OkObjectResult(new {data = items, count, pageNumber, pageSize });
		}
	}
}
