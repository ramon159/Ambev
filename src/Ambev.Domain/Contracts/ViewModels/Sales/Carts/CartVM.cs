using Ambev.Domain.Contracts.ViewModels.Common;
using Ambev.Shared.Entities.Sales.Carts;
using AutoMapper;

namespace Ambev.Domain.Contracts.ViewModels.Sales.Carts
{
    public class CartVM : BaseViewModel
    {
        public Guid? UserId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public List<CartProductVM>? Products { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CartVM, Cart>().ReverseMap();
            }
        }
    }
}