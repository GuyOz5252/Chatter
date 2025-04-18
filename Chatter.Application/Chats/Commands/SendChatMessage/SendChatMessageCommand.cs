using ICommand = SharedKernel.Interfaces.ICommand;

namespace Chatter.Application.Chats.Commands.SendChatMessage;

public record SendChatMessageCommand(Guid ChatId, Guid UserId, string ChatMessageContent) : ICommand;