using SharedKernel;

namespace Chatter.Domain.Entities;

public class Chat : EntityBase, IAggregateRoot
{
    private readonly List<ChatMessage> _chatMessages = [];
    private readonly List<User> _participants = [];

    public required Guid Id { get; init; }
    public IReadOnlyCollection<ChatMessage> ChatMessages => [.. _chatMessages];
    public IReadOnlyCollection<User> Participants => [.. _participants];
}
