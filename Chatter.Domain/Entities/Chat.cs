using SharedKernel.Interfaces;

namespace Chatter.Domain.Entities;

public class Chat(Guid chatId) : IAggregateRoot
{
    private readonly List<User> _participants = [];
    
    private readonly List<ChatMessage> _chatMessages = [];
    
    public Guid ChatId { get; init; } = chatId;

    public IReadOnlyCollection<User> Participants => _participants.AsReadOnly(); 

    public IReadOnlyCollection<ChatMessage> ChatMessages => _chatMessages.AsReadOnly();

    public void AddUser(User participant)
    {
        if (_participants.Contains(participant))
        {
            return;
        }
        
        _participants.Add(participant);
    }

    public void RemoveUser(User participant)
    {
        if (!_participants.Contains(participant))
        {
            return;
        }
        
        _participants.Remove(participant);
    }
    
    public void AddMessage(User sender, string messageContent)
    {
        var chatMessage = new ChatMessage(Guid.NewGuid(), sender, DateTime.Now, messageContent);
        _chatMessages.Add(chatMessage);
    }
}
