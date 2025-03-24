using SharedKernel.Interfaces;

namespace Chatter.Application.Chats.Commands.CreateChat;

public record CreateChatCommand(List<Guid> ParticipantIds) : ICommand<Guid>;