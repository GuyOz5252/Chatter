namespace Chatter.Domain.Entities;

public class ChatMessage
{
    public Guid ChatMessageId { get; init; }
    public User Sender { get; init; }
    public DateTime Timestamp { get; init; }
    public string MessageContent { get; init; }

    public ChatMessage(Guid chatMessageId, User sender, DateTime timestamp, string messageContent)
    {
        ChatMessageId = chatMessageId;
        Sender = sender;
        Timestamp = timestamp;
        MessageContent = messageContent;
    }

    public ChatMessage()
    {
        // EF Core
    }
}