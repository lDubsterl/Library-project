using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Application.Common.Validators;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Common;
using Library.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.Features.Authors.Commands
{
	public class EditAuthorCommand : BaseAuthor, IRequest<IActionResult>, IMapTo<Author>, IToValidate
	{
	}
	public class EditAuthorHandler : IRequestHandler<EditAuthorCommand, IActionResult>
	{
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		public EditAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IActionResult> Handle(EditAuthorCommand request, CancellationToken cancellationToken)
		{
			var author = await _unitOfWork.Repository<Author>().GetByIdAsync(request.Id);
			if (author is null)
				return new BadRequestObjectResult(new { Message = "Author not found" });

			var newAuthor = _mapper.Map<Author>(request);
			await _unitOfWork.Repository<Author>().UpdateAsync(newAuthor);
			await _unitOfWork.Save();

			return new OkObjectResult(new { data = request.Id, Message = $"{newAuthor.Name} {newAuthor.Surname} was updated successfully" });
		}
	}
}
