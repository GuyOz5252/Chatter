using SharedKernel;

namespace Chatter.Domain.Entities;

public class Chat : EntityBase, IAggregateRoot
{
    private readonly List<ChatMessage> _chatMessages;
    private readonly List<Guid> _participantsUserIds;
    
    public IReadOnlyCollection<ChatMessage> ChatMessages => _chatMessages;
    public IReadOnlyCollection<Guid> ParticipantsUserIds => _participantsUserIds;

    public Chat() {}
    
    public Chat(List<Guid> participantsUserIds)
    {
        Id = Guid.NewGuid();
        _participantsUserIds = participantsUserIds;
        _chatMessages = [];
    }

    public void SendMessage(ChatMessage chatMessage)
    {
        _chatMessages.Add(chatMessage);
    }

    public bool IsParticipant(Guid userId)
    {
        return _participantsUserIds.Contains(userId);
    }
}
