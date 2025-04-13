using Ambev.Domain.Entities.Authentication;
using Ambev.Domain.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;

namespace Ambev.Application.Features.Users.Queries.GetUser
{
    public class GetUserHandler : IRequestHandler<GetUserCommand, GetUserResponse>
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUserHandler(IRepositoryBase<User> userRepository, IMapper mapper)
        {
            _userRepository=userRepository;
            _mapper=mapper;
        }

        public async Task<GetUserResponse> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
            Guard.Against.NotFound(request.Id, entity);
            return _mapper.Map<GetUserResponse>(entity);
        }
    }
}
