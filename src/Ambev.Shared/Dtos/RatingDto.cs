using Ambev.Shared.Entities;
using AutoMapper;

namespace Ambev.Shared.Dtos
{
    public class RatingDto
    {
        public double Rate { get; set; }
        public int Count { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<RatingDto, Rating>().ReverseMap();
            }
        }
    }
}
