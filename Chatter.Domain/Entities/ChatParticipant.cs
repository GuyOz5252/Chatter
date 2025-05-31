using SharedKernel;

namespace Chatter.Domain.Entities;

public class ChatParticipant : EntityBase
{
    public required Guid UserId { get; init; }
    public required Guid ChatId { get; init; }
}
