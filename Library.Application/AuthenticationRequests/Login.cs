﻿using Library.Domain.Common;
using Library.Shared.Results;
using MediatR;

namespace Library.Application.AuthenticationRequests
{
    public class Login : IRequest<Result<Tokens>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
