namespace Library.Application.Common.BaseClasses
{
    public abstract class PaginationBase
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
