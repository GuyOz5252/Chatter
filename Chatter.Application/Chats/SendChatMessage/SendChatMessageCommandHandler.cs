using Chatter.Domain.Abstract;
using Chatter.Domain.Entities;
using SharedKernel;
using SharedKernel.Messaging;
using SharedKernel.Results;

namespace Chatter.Application.Chats.SendChatMessage;

public class SendChatMessageCommandHandler : ICommandHandler<SendChatMessageCommand>
{
    private readonly IChatRepository _chatRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public SendChatMessageCommandHandler(
        IChatRepository chatRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _chatRepository = chatRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> HandleAsync(SendChatMessageCommand command, CancellationToken cancellationToken = default)
    {
        var chat = await _chatRepository.GetFullAsync(command.ChatId, cancellationToken);
        if (chat.IsFailure)
        {
            return chat;
        }

        if (!chat.Value.IsParticipant(command.UserId))
        {
            return Error.Unauthorized("User is not participant in chat.");
        }

        var chatMessage = new ChatMessage
        {
            Id = Guid.NewGuid(),
            ChatId = command.ChatId,
            SenderUserId = command.UserId,
            MessageContent = command.MessageContent,
            SentAt = _dateTimeProvider.UtcNow
        };
        
        chat.Value.SendMessage(chatMessage);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return Result.Success();
    }
}
