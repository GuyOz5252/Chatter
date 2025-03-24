namespace Chatter.Api.Dtos;

public record CreateChatDto
{
    public required List<Guid> ParticipantIds { get; init; }
}