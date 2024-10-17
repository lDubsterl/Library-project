using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Library.Application.Features.Books.Commands
{
	public class AddImageCommand : IRequest<IActionResult>, IMapTo<Book>
	{
		public string ISBN { get; set; }
		public IFormFile Image { get; set; }
	}

	public class AddImageHandler : IRequestHandler<AddImageCommand, IActionResult>
	{
		private IUnitOfWork _unitOfWork;
		private string _publicFolder;
		public AddImageHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
		{
			_unitOfWork = unitOfWork;
			_publicFolder = config["FrontendPublicFolder"];
		}
		public async Task<IActionResult> Handle(AddImageCommand request, CancellationToken cancellationToken)
		{
			var book = await _unitOfWork.Repository<Book>().Entities.FirstOrDefaultAsync(b => request.ISBN == b.ISBN, cancellationToken);
			if (book == null)
				return new BadRequestObjectResult(new { Message = "Book not found" });

			var imagePath = await UploadImageToServer.Upload(request.ISBN, request.Image, _publicFolder);
			book.PathToImage = imagePath;

			await _unitOfWork.Repository<Book>().UpdateAsync(book);
			await _unitOfWork.Save();

			return new OkObjectResult(new { Message = "Book was updated succesfully" });
		}
	}
}
