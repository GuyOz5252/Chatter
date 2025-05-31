using SharedKernel;

namespace Chatter.Domain.Entities;

public class ChatMessage : EntityBase
{
    public required Guid SenderUserId { get; init; }
    public required string MessageContent { get; init; }
    public required DateTime SentAt { get; init; }
}
