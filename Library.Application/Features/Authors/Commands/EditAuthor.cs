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
