﻿using Ambev.Application.Exceptions;
using Ambev.Domain.Entities.Authentication;
using Ambev.Domain.Interfaces.Infrastructure.Repositories;
using Ambev.Domain.Interfaces.Infrastructure.Security;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserHandler(IMapper mapper, IRepositoryBase<User> userRepository, IPasswordHasher passwordHasher)
        {
            _mapper = mapper;
            _userRepository=userRepository;
            _passwordHasher=passwordHasher;
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

            user.PasswordHash = _passwordHasher.HashPassword(request.Password);

            var result = await _userRepository.AddAsync(user, cancellationToken);

            return _mapper.Map<CreateUserResponse>(result);
        }
    }
}
