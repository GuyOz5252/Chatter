namespace Chatter.Domain.Entities;

public class ChatMessage
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; init; } =  DateTime.UtcNow;
    // public Guid SenderId { get; init; }
    public User Sender { get; init; }
    public string MessageContent { get; private set; }

#pragma warning disable CS8618
    public ChatMessage()
    {
    }
#pragma warning restore CS8618
    
    public ChatMessage(User sender, string messageContent)
    {
        Sender = sender;
        MessageContent = messageContent;
    }
}