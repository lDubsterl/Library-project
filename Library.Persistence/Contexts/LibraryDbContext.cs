﻿using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Contexts
{
    public class LibraryDbContext: DbContext
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<User> Users => Set<User>();
        public DbSet<UserBook> UserBooks => Set<UserBook>();
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
	}
}
