using Library.Application.Interfaces.Repositories;
using Library.Persistence.Contexts;
using Library.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Persistence.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext(configuration);
			services.AddRepositories();
		}

		public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration["Connection"];

			services.AddDbContext<LibraryDbContext>(options =>
			   options.UseSqlServer(connectionString));
		}

		private static void AddRepositories(this IServiceCollection services)
		{
			services
				.AddTransient<IUnitOfWork, UnitOfWork>()
				.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
		}
	}
}
