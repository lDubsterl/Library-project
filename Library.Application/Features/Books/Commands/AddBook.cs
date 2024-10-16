using AutoMapper;
using Library.Domain.Entities;
using MediatR;
using Library.Application.Interfaces.Repositories;
using Library.Shared.Results;
using Library.Domain.Common;
using Library.Application.Common.Mappings;
using Library.Application.DTOs;
using Library.Application.Common.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Library.Application.Features.Books.Commands
{
	public class AddBookCommand : BaseBook, IRequest<Result<int>>, IMapTo<Book>, IMapTo<BookDTO>, IToValidate
	{
		public int AuthorId { get; set; }
		public IFormFile Image { get; set; } = null;
	}
	public class AddBookHandler : IRequestHandler<AddBookCommand, Result<int>>
	{
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		private string _publicFolder;
		public AddBookHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_publicFolder = config["FrontendPublicFolder"];
		}
		bool IsEqual(Book fromDb, AddBookCommand fromRequest) => _mapper.Map<BookDTO>(fromDb).Equals(_mapper.Map<BookDTO>(fromRequest));
		public async Task<Result<int>> Handle(AddBookCommand request, CancellationToken cancellationToken)
		{
			var author = _unitOfWork.Repository<Author>().Entities.FirstOrDefault(obj => obj.Id == request.AuthorId);

			if (author == null)
				return await Result<int>.FailureAsync("Author not found");

			var isBookExists = _unitOfWork.Repository<Book>().Entities
				.AsEnumerable()
				.Any(a => IsEqual(a, request));

			if (isBookExists)
				return await Result<int>.FailureAsync("Book with this ISBN already exists");
			
			var book = _mapper.Map<Book>(request);
			book.Author = author;

			var writeTask = Task.CompletedTask;
			string imagePath = string.Empty;
			if (request.Image is not null)
			{
				imagePath = _publicFolder + $"/BookImages/{request.ISBN}.png";
				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					writeTask = request.Image.CopyToAsync(stream, cancellationToken);
				}
				imagePath = Path.GetRelativePath(_publicFolder, imagePath);
			}
			book.PathToImage = imagePath;

			book = _unitOfWork.Repository<Book>().Add(book);
			await _unitOfWork.Save();
			await writeTask;

			return await Result<int>.SuccessAsync(book.Id, $"{book.Title} was added successfully");
		}
	}
}
