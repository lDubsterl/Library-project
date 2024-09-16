using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Application.DTOs;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Authors.Commands
{
	public class DeleteAuthorCommand : BaseAuthor, IRequest<Result<int>>, IMapTo<Author>
	{
		public int Id { get; set; }
	}
	public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, Result<int>>
	{
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		public DeleteAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		bool IsEqual(Author fromDb, AddAuthorCommand fromRequest) => _mapper.Map<AuthorDTO>(fromDb) == _mapper.Map<AuthorDTO>(fromRequest);
		public async Task<Result<int>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
		{
			var author = _mapper.Map<Author>(request);
			var authorFromDb = await _unitOfWork.Repository<Author>().GetByIdAsync(request.Id);
			if (authorFromDb is null)
				return await Result<int>.FailureAsync("Author doesn't exist");
			await _unitOfWork.Repository<Author>().DeleteAsync(author);
			await _unitOfWork.Save();
			return Result<int>.Success(request.Id, $"{author.Name} {author.Surname} was deleted successfully");
		}
	}
}
