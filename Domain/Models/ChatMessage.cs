namespace Domain.Models;

public record ChatMessage(Guid ChatMessageId, User Sender, DateTime Timestamp, string MessageContent);