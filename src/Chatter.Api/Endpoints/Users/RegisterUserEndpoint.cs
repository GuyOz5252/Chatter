using Chatter.Api.Endpoints.Abstract;
using Chatter.Api.Extensions;
using Chatter.Application.Users.Register;
using SharedKernel.Messaging;

namespace Chatter.Api.Endpoints.Users;

public class RegisterUserEndpoint : IEndpoint
{
    private sealed record RegisterUserRequest(string UserName, string Email, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/users", async (
                RegisterUserRequest request,
                ICommandHandler<RegisterUserCommand, Guid> commandHandler,
                CancellationToken cancellationToken) =>
            {
                var registerUserCommand = new RegisterUserCommand
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    Password = request.Password
                };
                var result = await commandHandler.HandleAsync(registerUserCommand, cancellationToken);
                return result.Match(
                    Results.Ok,
                    error => error.ToProblemDetails());
            })
            .WithTags(Tags.Users);
    }
}
