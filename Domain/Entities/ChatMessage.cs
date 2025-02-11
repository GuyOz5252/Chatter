namespace Domain.Entities;

public record ChatMessage(Guid ChatMessageId, User Sender, DateTime Timestamp, string MessageContent);