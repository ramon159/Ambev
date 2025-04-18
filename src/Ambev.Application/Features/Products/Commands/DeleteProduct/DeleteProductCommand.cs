﻿using Ambev.Domain.Attributes.Security;
using Ambev.Domain.Constants;
using MediatR;

namespace Ambev.Application.Features.Products.Commands.DeleteProduct
{
    [Authorize(Roles = Roles.Admin)]
    public class DeleteProductCommand : IRequest<DeleteProductResponse>
    {
        public Guid Id { get; set; }
    }
}