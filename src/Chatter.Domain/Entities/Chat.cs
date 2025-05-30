using SharedKernel;

namespace Chatter.Domain.Entities;

public class Chat : EntityBase, IAggregateRoot
{
    private readonly List<ChatMessage> _chatMessages;
    private readonly List<ChatParticipant> _participants;
    
    public List<ChatMessage> ChatMessages => _chatMessages;
    public List<ChatParticipant> Participants => _participants;

    public Chat() {}
    
    public Chat(List<Guid> participantsUserIds)
    {
        Id = Guid.NewGuid();
        _participants = participantsUserIds.Select(userId => new ChatParticipant
        {
            UserId = userId,
            ChatId = Id
        }).ToList();
        _chatMessages = [];
    }

    public void SendMessage(ChatMessage chatMessage)
    {
        _chatMessages.Add(chatMessage);
    }

    public bool IsParticipant(Guid userId)
    {
        return _participants.Any(chatParticipant => chatParticipant.UserId == userId);
    }
}
