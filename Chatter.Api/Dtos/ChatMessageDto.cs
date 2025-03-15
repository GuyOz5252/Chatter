namespace Chatter.Api.Dtos;

public record ChatMessageDto
{
    public required Guid UserId { get; init; }
    
    public required string ChatMessageContent { get; init; }
}