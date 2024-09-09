using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Authors.Commands
{
    internal class EditAuthorCommand : Author, IRequest<Result<int>> { }
	internal class EditAuthorHandler : IRequestHandler<EditAuthorCommand, Result<int>>
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
			var author = _mapper.Map<Author>(request);
			await _unitOfWork.Repository<Author>().UpdateAsync(author);
			return Result<int>.Success($"{author.Name} {author.Surname} was updated successfully");
		}
	}
}
