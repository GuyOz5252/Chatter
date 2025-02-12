using SharedKernel.Interfaces;

namespace Chatter.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Username, string Email) : ICommand<Guid>;
