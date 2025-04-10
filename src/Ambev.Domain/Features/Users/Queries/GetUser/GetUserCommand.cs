using MediatR;

namespace Ambev.Domain.Features.Users.Queries.GetUser
{
    public class GetUserCommand : IRequest<GetUserResponse>
    {
        public Guid Id { get; set; }
    }
}
