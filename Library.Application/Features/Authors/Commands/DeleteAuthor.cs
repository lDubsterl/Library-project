using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Application.DTOs;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Common;
using Library.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.Features.Authors.Commands
{
	public class DeleteAuthorCommand : BaseAuthor, IRequest<IActionResult>, IMapTo<Author>
	{
	}
	public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, IActionResult>
	{
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		public DeleteAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		bool IsEqual(Author fromDb, AddAuthorCommand fromRequest) => _mapper.Map<AuthorDTO>(fromDb) == _mapper.Map<AuthorDTO>(fromRequest);
		public async Task<IActionResult> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
		{
			var author = _mapper.Map<Author>(request);
			var authorFromDb = await _unitOfWork.Repository<Author>().GetByIdAsync(request.Id);
			if (authorFromDb is null)
				return new BadRequestObjectResult(new { Message = "Book not found" });
			await _unitOfWork.Repository<Author>().DeleteAsync(author);
			await _unitOfWork.Save();

			return new OkObjectResult(new { Message = $"{author.Name} {author.Surname} was deleted successfully" });
		}
	}
}
