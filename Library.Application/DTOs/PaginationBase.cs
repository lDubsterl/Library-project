using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTOs
{
	public abstract class PaginationBase
	{
		protected PaginationBase(int pageNumber, int pageSize)
		{
			PageNumber = pageNumber;
			PageSize = pageSize;
		}

		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
