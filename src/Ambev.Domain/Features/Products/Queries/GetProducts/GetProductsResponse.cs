using Ambev.Shared.Common.Http;
using Ambev.Shared.Entities;
using AutoMapper;

namespace Ambev.Domain.Features.Products.Queries.GetProducts
{
    public interface IPaginatedResponse<T>
    {
        public IReadOnlyCollection<T> Items { get; }
        public int TotalItems { get; }
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }
    }
    public class GetProductsResponse : IPaginatedResponse<Product>
    {
        public IReadOnlyCollection<Product> Items { get; }

        public int TotalItems { get; }

        public int CurrentPage { get; }

        public int TotalPages { get; }

        public bool HasPreviousPage { get; }

        public bool HasNextPage { get; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<GetProductsResponse, PaginedList<Product>>().ReverseMap();
            }
        }
    }
}
