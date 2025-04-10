using Ambev.Shared.Common.Exceptions;
using Ambev.Shared.Entities.Authentication;
using Ambev.Shared.Entities.Sales;
using Ambev.Shared.Interfaces.Infrastructure.Repositories;
using Ambev.Shared.Interfaces.Infrastructure.Security;
using Ambev.Shared.ValueObjects;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Domain.Features.Users.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateUserHandler(IMapper mapper, IRepositoryBase<User> userRepository, IPasswordHasher passwordHasher)
        {
            _mapper = mapper;
            _userRepository=userRepository;
            _passwordHasher=passwordHasher;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            Guard.Against.NotFound(request.Id, user);

            var alreadyExists = await _userRepository.DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id != request.Id & (u.Email == request.Email || u.UserName == request.UserName), cancellationToken);

            if (alreadyExists != null)
            {
                var duplicatedFields = new List<string>();

                if (alreadyExists.Email == request.Email)
                    duplicatedFields.Add("Email");

                if (alreadyExists.UserName == request.UserName)
                    duplicatedFields.Add("UserName");

                throw new BusinessValidationException($"{string.Join(" and ", duplicatedFields)} already in use.");
            }


            if (request.Password != null) 
            {
                user.PasswordHash = _passwordHasher.HashPassword(request.Password);
            }

            user.Email = request.Email;
            user.UserName = request.UserName;
            user.Name = request.Name;
            user.Address = request.Address;
            user.Phone = request.Phone;
            user.Status = request.Status;
            user.Role = request.Role;

            var result = await _userRepository.UpdateAsync(user, cancellationToken);

            return _mapper.Map<UpdateUserResponse>(result);
        }
    }
}
