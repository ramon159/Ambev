using Ambev.Domain.Entities.Authentication;
using Ambev.Domain.Interfaces.Infrastructure.Repositories;
using Ardalis.GuardClauses;
using MediatR;

namespace Ambev.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
    {
        private readonly IRepositoryBase<User> _userRepository;

        public DeleteUserHandler(IRepositoryBase<User> userRepository)
        {
            _userRepository=userRepository;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.GetByIdAsync(
                request.Id
                );

            Guard.Against.NotFound(request.Id, entity);

            await _userRepository.DeleteAsync(entity);

            return new DeleteUserResponse { Success = true };
        }
    }
}
