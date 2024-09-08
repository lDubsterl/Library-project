using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Contexts
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();

        ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        {
            Database.EnsureCreated();
        }
	}
}
