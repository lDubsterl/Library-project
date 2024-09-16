using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Authors.Commands
{
	public class EditAuthorCommand : BaseAuthor, IRequest<Result<int>>, IMapTo<Author>
	{
		public int Id { get; set; }
	}
	public class EditAuthorHandler : IRequestHandler<EditAuthorCommand, Result<int>>
	{
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		public EditAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<int>> Handle(EditAuthorCommand request, CancellationToken cancellationToken)
		{
			var author = await _unitOfWork.Repository<Author>().GetByIdAsync(request.Id);
			if (author is null)
				return await Result<int>.FailureAsync("Author not found");

			var newAuthor = _mapper.Map<Author>(request);
			await _unitOfWork.Repository<Author>().UpdateAsync(newAuthor);
			await _unitOfWork.Save();
			return await Result<int>.SuccessAsync(request.Id, $"{newAuthor.Name} {newAuthor.Surname} was updated successfully");
		}
	}
}
