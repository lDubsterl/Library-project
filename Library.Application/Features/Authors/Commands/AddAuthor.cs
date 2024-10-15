using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Application.Common.Validators;
using Library.Application.DTOs;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.Features.Authors.Commands
{
	public class AddAuthorCommand : BaseAuthor, IRequest<Result<int>>, IMapTo<Author>, IMapTo<AuthorDTO>, IToValidate
	{
	}

	public class AddAuthorHandler : IRequestHandler<AddAuthorCommand, Result<int>>
	{
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		public AddAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		bool IsEqual(Author fromDb, AddAuthorCommand fromRequest) => _mapper.Map<AuthorDTO>(fromDb) == _mapper.Map<AuthorDTO>(fromRequest);
		public async Task<Result<int>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
		{
			var isAuthorExists = _unitOfWork.Repository<Author>().Entities
				.AsEnumerable()
				.Any(a => IsEqual(a, request));
			if (!isAuthorExists)
			{
				var author = _mapper.Map<Author>(request);
				author = _unitOfWork.Repository<Author>().Add(author);
				await _unitOfWork.Save();
				return await Result<int>.SuccessAsync(author.Id, $"{author.Name} {author.Surname} was added successfully");
			}
			else
				return await Result<int>.FailureAsync("Author already exists");
		}
	}
}
