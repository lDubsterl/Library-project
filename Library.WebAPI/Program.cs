using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Library.Infrastructure;
using Library.Application.Interfaces.Services;
using Library.Infrastructure.Services;
using Library.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Library.Application.Extensions;
using Library.Infrastructure.Extensions;
using Library.Persistence.Extensions;
using FluentValidation.AspNetCore;
using FluentValidation;
using Library.Application.Common.Exceptions;

namespace Library.WebAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddApplicationLayer();
			builder.Services.AddInfrastructureLayer();
			builder.Services.AddPersistenceLayer(builder.Configuration);

			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = TokenBuilder.Issuer,
						ValidAudience = TokenBuilder.Audience,
						IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(TokenBuilder.Secret))
					};
				});

			builder.Services.AddAuthorizationBuilder()
				.AddPolicy("AdminsOnly", policy =>
			policy.RequireRole("admin"));

			builder.Services.AddCors(options =>
			{
				options.AddDefaultPolicy(builder =>
					builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					);
			});

			var app = builder.Build();
			Directory.CreateDirectory($"{app.Configuration["FrontendPublicFolder"]}/BookImages");

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseMiddleware<ExceptionMiddleware>();

			app.UseHttpsRedirection();

			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllers();

			app.UseCors();

			app.Run();
		}
	}
}
