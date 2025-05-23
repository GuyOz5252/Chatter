using SharedKernel;

namespace Chatter.Domain.Entities;

public class User : EntityBase, IAggregateRoot
{
    public required Guid Id { get; init; }
    public required string UserName { get; init; }
    public required string Email { get; init; }
    public required string PasswordHash { get; init; }
}
