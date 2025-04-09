using Ambev.Domain.Contracts.ViewModels.Common;
using Ambev.Shared.Entities.Sales;
using AutoMapper;

namespace Ambev.Domain.Contracts.ViewModels.Sales.Carts
{
    public class CartVM : BaseViewModel
    {
        public Guid? UserId { get; set; }
        public DateTimeOffset? Date { get; set; }
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