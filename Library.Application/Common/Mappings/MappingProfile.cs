using AutoMapper;
using Library.Application.Features.Authors.Commands;
using Library.Domain.Entities;
using System.Reflection;

namespace Library.Application.Common.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
		}

		private void ApplyMappingsFromAssembly(Assembly assembly)
		{
			var mapTypes = new Type[] { typeof(IMapFrom<>), typeof(IMapTo<>) };

			var mappingMethodName = nameof(IMapFrom<object>.Mapping);

			bool HasInterface(Type t)
			{
				foreach (var mapType in mapTypes)
					if (t.IsGenericType && t.GetGenericTypeDefinition() == mapType)
						return true;
				return false;
			}

			var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

			var argumentTypes = new Type[] { typeof(Profile) };

			foreach (var type in types)
			{
				var instance = Activator.CreateInstance(type);

				var methodInfo = type.GetMethod(mappingMethodName);

				if (methodInfo != null)
				{
					methodInfo.Invoke(instance, [this]);
				}
				else
				{
					var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

					if (interfaces.Count > 0)
					{
						foreach (var @interface in interfaces)
						{
							var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

							interfaceMethodInfo.Invoke(instance, [this]);
						}
					}
				}
			}
		}
	}
}
