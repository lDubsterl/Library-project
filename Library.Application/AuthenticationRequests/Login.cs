﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.AuthenticationRequests
{
	public class Login : IRequest<IActionResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
