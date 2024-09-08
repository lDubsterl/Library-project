using AutoMapper;
using Library.Application.Features.Books.Commands;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.Authors.Commands
{
	internal class AddAuthorCommand: IRequest<Result<int>>
	{
	}
	internal class AddAuthorHandler
	{
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		public AddAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<int>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
		{
			var author = _mapper.Map<Author>(request);
			_unitOfWork.Repository<Author>().Add(author);
			await _unitOfWork.Save();
			return Result<int>.Success($"{author.Name} {author.Surname} was added successfully");
		}
	}
}
