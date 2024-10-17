using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Application.Common.Validators;
using Library.Application.DTOs;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Common;
using Library.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.Features.Authors.Commands
{
	public class AddAuthorCommand : BaseAuthor, IRequest<IActionResult>, IMapTo<Author>, IMapTo<AuthorDTO>, IToValidate
	{
	}

	public class AddAuthorHandler : IRequestHandler<AddAuthorCommand, IActionResult>
	{
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		public AddAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		bool IsEqual(Author fromDb, AddAuthorCommand fromRequest) => _mapper.Map<AuthorDTO>(fromDb) == _mapper.Map<AuthorDTO>(fromRequest);
		public async Task<IActionResult> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
		{
			var isAuthorExists = _unitOfWork.Repository<Author>().Entities
				.Any(a => a.Id == request.Id);
			if (!isAuthorExists)
			{
				var author = _mapper.Map<Author>(request);
				author = _unitOfWork.Repository<Author>().Add(author);
				await _unitOfWork.Save();
				return new OkObjectResult(new { data = author.Id, Message = $"{author.Name} {author.Surname} was added successfully" });
			}
			else
				return new BadRequestObjectResult(new { message = "Author already exists" });
		}
	}
}
