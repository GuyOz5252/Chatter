namespace Chatter.Client.Models;

public class ChatMessage
{
    public required string Sender { get; init; }
    
    public required string MessageContent { get; init; }
    
    public required DateTime SentAt { get; init; }
}