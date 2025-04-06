//using Ambev.Shared.Entities.Authentication;
//using Ambev.Shared.Interfaces.Infrastructure.Repositories;
//using AutoMapper;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Ambev.Domain.Features.Authentication.Commands.CreateUser
//{
//    public class AuthUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
//    {
//        private readonly IMapper _mapper;
//        private readonly IRepositoryBase<User> _userRepository;

//        public AuthUserHandler(IMapper mapper, IRepositoryBase<User> userRepository)
//        {
//            _mapper = mapper;
//            _userRepository = userRepository;
//        }

//        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
//        {
//            var user = _mapper.Map<User>(request);

//            var result = await _userRepository.AddAsync(user, cancellationToken);

//            return _mapper.Map<CreateUserResponse>(result);
//        }
//    }
//}
