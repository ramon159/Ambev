using Ambev.Application.Features.Users.Queries.GetUser;
using Ambev.Application.Models.Http;
using Ambev.Domain.Entities.Authentication;
using Ambev.Domain.Interfaces.Infrastructure.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.Application.Features.Users.Queries.GetUsersWithPagination
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUserCommand, PaginedList<GetUserResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<User> _userRepository;

        public GetAllUsersHandler(IMapper mapper, IRepositoryBase<User> userRepository)
        {
            _mapper=mapper;
            _userRepository=userRepository;
        }

        public async Task<PaginedList<GetUserResponse>> Handle(GetAllUserCommand request, CancellationToken cancellationToken)
        {
            var paginedResult = await _userRepository.GetAllAsync(
                page: request.Page,
                pageSize: request.Size,
                sortTerm: request.Order,
                filters: request.Filters,
                cancellationToken: cancellationToken
            );

            var items = _mapper.Map<List<GetUserResponse>>(paginedResult.Items);

            return new PaginedList<GetUserResponse>(items, paginedResult.Count, request.Page, request.Size);
        }
    }
}
