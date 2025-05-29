using Chatter.Api.Endpoints.Abstract;
using Chatter.Api.Extensions;
using Chatter.Application.Chats.Create;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using SharedKernel.Messaging;

namespace Chatter.Api.Endpoints.Chats;

public class CreateChatEndpoint : IEndpoint
{
    private sealed record Request(string ChatName, List<Guid> AdminUserIds, List<Guid> ParticipantsUserIds);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/chats", async (
                ICommandHandler<CreateChatCommand, Guid> commandHandler,
                Request request,
                CancellationToken cancellationToken) =>
            {
                var createChatCommand = new CreateChatCommand
                {
                    ChatName = request.ChatName,
                    AdminUserIds = request.AdminUserIds,
                    ParticipantsUserIds = request.ParticipantsUserIds
                };
                var result = await commandHandler.HandleAsync(createChatCommand, cancellationToken);
                result.Match(
                    Results.Ok,
                    error => error.ToProblemDetails());
            })
            .WithTags(Tags.Chats);
    }
}
