using ICommand = SharedKernel.Interfaces.ICommand;

namespace Chatter.Application.Chat.Commands.SendChatMessage;

public record SendChatMessageCommand(Guid UserId, Guid ChatId, string ChatMessageContent) : ICommand;