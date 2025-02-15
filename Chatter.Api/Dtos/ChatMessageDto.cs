namespace Chatter.Api.Dtos;

public record ChatMessageDto(Guid UserId, string ChatMessageContent);