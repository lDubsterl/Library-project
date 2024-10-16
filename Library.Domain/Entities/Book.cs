﻿
using Library.Domain.Common;
using Library.Domain.Interfaces;

namespace Library.Domain.Entities
{
    public class Book: BaseBook, IEntity
    {
		public int Id { get; set; }
		public required Author Author { get; set; }
		public string PathToImage { get; set; } = string.Empty;
		public List<UserBook> UserBooks { get; set; }
	}
}
