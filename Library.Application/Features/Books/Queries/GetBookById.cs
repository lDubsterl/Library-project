using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.Features.Books.Queries
{
	public class GetBookById : IRequest<IActionResult>
	{
		public int Id { get; set; }
	}

	public class GetBookByIdHandler : IRequestHandler<GetBookById, IActionResult>
	{
		IUnitOfWork _unitOfWork;
		IMapper _mapper;

		public GetBookByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IActionResult> Handle(GetBookById query, CancellationToken cancellationToken)
		{
			var book = await _unitOfWork.Repository<Book>()
				.GetByIdAsync(query.Id);
			var mappedBook = _mapper.Map<BookDTO>(book);
			return new OkObjectResult(new { Data = mappedBook });
		}
	}
}
