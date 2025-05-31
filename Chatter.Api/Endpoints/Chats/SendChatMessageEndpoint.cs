using Chatter.Api.Endpoints.Abstract;
using Chatter.Api.Extensions;
using Chatter.Application.Chats.SendChatMessage;
using SharedKernel.Messaging;

namespace Chatter.Api.Endpoints.Chats;

public class SendChatMessageEndpoint : IEndpoint
{
    private sealed record SendChatMessageRequest(Guid UserId, Guid ChatId, string MessageContent);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/chats/send-message", async (
                SendChatMessageRequest request,
                ICommandHandler<SendChatMessageCommand> commandHandler,
                CancellationToken cancellationToken) =>
            {
                var sendChatMessageCommand = new SendChatMessageCommand
                {
                    UserId = request.UserId,
                    ChatId = request.ChatId,
                    MessageContent = request.MessageContent
                };
                var result = await commandHandler.HandleAsync(sendChatMessageCommand, cancellationToken);
                return result.Match(
                    () => Results.Ok(),
                    error => error.ToProblemDetails());
            })
            .WithTags(Tags.Chats);
    }
}
