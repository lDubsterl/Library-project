using Library.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Features.Books.Commands
{
	public static class UploadImageToServer
	{
		public static async Task<string> Upload(string bookISBN, IFormFile image, string folder)
		{
			var task = Task.CompletedTask;
			string imagePath = string.Empty;
			if (image is not null)
			{
				imagePath = folder + $"/BookImages/{bookISBN}.png";
				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					task = image.CopyToAsync(stream);
				}
				imagePath = Path.GetRelativePath(folder, imagePath);
			}
			await task;
			return imagePath;
		}
	}
}
