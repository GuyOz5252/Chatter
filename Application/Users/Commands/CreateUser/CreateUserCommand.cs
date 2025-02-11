using SharedKernel.Interfaces;

namespace Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Username, string Email) : ICommand<Guid>;
