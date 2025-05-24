using SharedKernel.Messaging;

namespace Chatter.Application.Chats.Create;

public record CreateChatCommand : ICommand<Guid>
{
    public required string ChatName { get; init; }
    public required List<Guid> AdminUserId { get; init; }
    public required List<Guid> ParticipantsUserIds { get; init; }
}
