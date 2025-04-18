﻿using Ambev.Application.Features.Products.Queries.GetProduct;
using Ambev.Application.Models.Http;
using MediatR;

namespace Ambev.Application.Features.Products.Queries.GetProductWithPagination
{
    public class GetAllProductsQuery : QueryParameters, IRequest<PaginedList<GetProductResponse>>
    {
    }
}
