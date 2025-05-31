using SharedKernel.Messaging;

namespace Chatter.Application.Chats.SendChatMessage;

public record SendChatMessageCommand : ICommand 
{
    public Guid UserId { get; init; }
    public Guid ChatId { get; init; }
    public string MessageContent { get; init; }
}
