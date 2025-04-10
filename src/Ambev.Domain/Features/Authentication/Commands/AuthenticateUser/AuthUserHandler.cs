//using Ambev.Shared.Entities.Authentication;
//using Ambev.Shared.Interfaces.Infrastructure.Repositories;
//using Ambev.Shared.Interfaces.Infrastructure.Security;
//using AutoMapper;

//namespace Ambev.Domain.Features.Authentication.Commands.AuthenticateUser
//{
//    public class AuthUserHandler
//    //: IRequestHandler<AuthUserCommand, AuthUserResponse>
//    {
//        private readonly IMapper _mapper;
//        private readonly IRepositoryBase<User> _userRepository;
//        private readonly ITokenService _tokenService;
//        private readonly IPasswordHasher passwordHasher;

//        public AuthUserHandler(IMapper mapper, IRepositoryBase<User> userRepository, ITokenService tokenService, IPasswordHasher passwordHasher)
//        {
//            _mapper = mapper;
//            _userRepository = userRepository;
//            _tokenService=tokenService;
//            this.passwordHasher=passwordHasher;
//        }

//        //public async Task<AuthUserResponse> Handle(AuthUserCommand request, CancellationToken cancellationToken)
//        //{
//        //    var user = await _userRepository.DbSet.FirstOrDefaultAsync(u => u.UserName == request.Username, cancellationToken);

//        //    if (user == null || passwordHasher.VerifyPassword(request.Password, user.Password))
//        //    {
//        //        throw new UnauthorizedAccessException("Invalid username or password");
//        //    }

//        //    switch (user.Status)
//        //    {
//        //        case UserStatus.Pending:
//        //            await SendEmailConfirmation();
//        //            throw new UnauthorizedAccessException("Account pending confirmation. Please check your email.");

//        //        case UserStatus.Locked:
//        //            throw new UnauthorizedAccessException("Account locked. Contact support.");

//        //        case UserStatus.Expired:
//        //            throw new UnauthorizedAccessException("Account expired. Contact support.");

//        //        case UserStatus.Disabled:
//        //            throw new UnauthorizedAccessException("Account disabled.");

//        //        case UserStatus.Active:
//        //            var token = _tokenService.GenerateToken(user);
//        //            return new AuthUserResponse { Token = token };
//        //        default:
//        //            throw new UnauthorizedAccessException("User is not active.");
//        //    }

//        //}

//        private async Task SendEmailConfirmation()
//        {
//            await Task.CompletedTask;
//        }
//    }
//}