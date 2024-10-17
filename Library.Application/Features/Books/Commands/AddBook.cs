using AutoMapper;
using Library.Domain.Entities;
using MediatR;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Common;
using Library.Application.Common.Mappings;
using Library.Application.DTOs;
using Library.Application.Common.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.Features.Books.Commands
{
	public class AddBookCommand : BaseBook, IRequest<IActionResult>, IMapTo<Book>, IMapTo<BookDTO>, IToValidate
	{
		public int AuthorId { get; set; }
		public IFormFile Image { get; set; } = null;
	}
	public class AddBookHandler : IRequestHandler<AddBookCommand, IActionResult>
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
		public async Task<IActionResult> Handle(AddBookCommand request, CancellationToken cancellationToken)
		{
			var author = _unitOfWork.Repository<Author>().Entities.FirstOrDefault(obj => obj.Id == request.AuthorId);

			if (author == null)
				return new BadRequestObjectResult(new { Message = "Author not found" });

			var isBookExists = _unitOfWork.Repository<Book>().Entities
				.Any(b => b.ISBN == request.ISBN);

			if (isBookExists)
				return new BadRequestObjectResult(new { Message = "Book with this ISBN already exists" });

			var book = _mapper.Map<Book>(request);
			book.Author = author;

			var imagePath = await UploadImageToServer.Upload(request.ISBN, request.Image, _publicFolder);
			book.PathToImage = imagePath;

			book = _unitOfWork.Repository<Book>().Add(book);
			await _unitOfWork.Save();

			return new OkObjectResult(new { data = book.Id, Message = $"{book.Title} was added successfully" });
		}
	}
}
