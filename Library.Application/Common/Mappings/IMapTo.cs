using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Mappings
{
	public interface IMapTo<T>
	{
		public void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
	}
}
