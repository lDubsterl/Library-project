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
	internal class EditBookCommand : Book, IRequest<Result<int>> { }
	internal class EditBookHandler : IRequestHandler<EditBookCommand, Result<int>>
	{
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		public EditBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<int>> Handle(EditBookCommand request, CancellationToken cancellationToken)
		{
			var book = _mapper.Map<Book>(request);
			await _unitOfWork.Repository<Book>().UpdateAsync(book);
			return Result<int>.Success($"{book.Title} was updated successfully");
		}
	}
}
