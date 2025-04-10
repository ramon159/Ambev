namespace Ambev.Domain.Contracts.Dtos.User
{
    public class UserDto
    {
        public string? Email { get; set; }
        public virtual string? UserName { get; set; }
        public virtual string? Password { get; set; }
    }
}