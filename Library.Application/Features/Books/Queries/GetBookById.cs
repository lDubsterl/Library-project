using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.Books.Queries
{
	internal class GetBookByIdQuery : IRequest<Result<Book>>
	{
		public GetBookByIdQuery(int id)
		{
			Id = id;
		}
		public int Id { get; set; }
	}

	internal class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Result<Book>>
	{
		IUnitOfWork _unitOfWork;
		IMapper _mapper;

		public GetBookByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<Book>> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
		{
			var book = await _unitOfWork.Repository<Book>().GetByIdAsync(query.Id);
			return await Result<Book>.SuccessAsync(book!);
		}
	}
}
