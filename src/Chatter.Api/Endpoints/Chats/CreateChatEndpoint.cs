using Chatter.Api.Endpoints.Abstract;
using Chatter.Api.Extensions;
using Chatter.Application.Chats.Create;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Messaging;

namespace Chatter.Api.Endpoints.Chats;

public class CreateChatEndpoint : IEndpoint
{
    private sealed record CreateChatRequest(string ChatName, List<Guid> AdminUserIds, List<Guid> ParticipantsUserIds);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/chats", async (
                CreateChatRequest request,
                ICommandHandler<CreateChatCommand, Guid> commandHandler,
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
