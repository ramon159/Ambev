using Ambev.Shared.Common.Exceptions;
using Ambev.Shared.Entities.Authentication;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Domain.Features.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<User> _userRepository;

        public CreateUserHandler(IMapper mapper, IRepositoryBase<User> userRepository)
        {
            _mapper = mapper;
            _userRepository=userRepository;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            var alreadyExists = await _userRepository.DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == request.Email || u.UserName == request.UserName, cancellationToken);

            if (alreadyExists != null)
            {
                var duplicatedFields = new List<string>();

                if (alreadyExists.Email == request.Email)
                    duplicatedFields.Add("Email");

                if (alreadyExists.UserName == request.UserName)
                    duplicatedFields.Add("UserName");

                throw new BusinessValidationException($"{string.Join(" and ", duplicatedFields)} already in use.");
            }


            var result = await _userRepository.AddAsync(user, cancellationToken);

            return _mapper.Map<CreateUserResponse>(result);
        }
    }
}
