using SharedKernel;

namespace Chatter.Application.Users.Register;

public record RegisterUserCommand : ICommand<Guid>
{
    public required string UserName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}
