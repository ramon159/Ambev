﻿using Ambev.Domain.Attributes.Security;
using Ambev.Domain.Constants;
using MediatR;

namespace Ambev.Application.Features.Users.Queries.GetUser
{
    [Authorize(Roles = Roles.Admin)]
    public class GetUserQuery : IRequest<GetUserResponse>
    {
        public Guid Id { get; set; }
    }
}
