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

namespace Library.Application.Features.Books.Commands
{
	internal class DeleteBookCommand : Book, IRequest<Result<int>> { }
	internal class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Result<int>>
	{
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		public DeleteBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<int>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
		{
			var book = _mapper.Map<Book>(request);
			await _unitOfWork.Repository<Book>().DeleteAsync(book);
			return Result<int>.Success($"{book.Title} was deleted successfully");
		}
	}
}
