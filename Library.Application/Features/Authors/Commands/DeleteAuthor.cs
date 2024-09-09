﻿using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Shared;
using MediatR;

namespace Library.Application.Features.Authors.Commands
{
	internal class DeleteAuthorCommand : Author, IRequest<Result<int>> { }
	internal class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, Result<int>>
	{
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		public DeleteAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<int>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
		{
			var author = _mapper.Map<Author>(request);
			await _unitOfWork.Repository<Author>().DeleteAsync(author);
			return Result<int>.Success($"{author.Name} {author.Surname} was deleted successfully");
		}
	}
}
