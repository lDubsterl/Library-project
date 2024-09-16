using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.BaseClasses
{
    public abstract class PaginationBase
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
