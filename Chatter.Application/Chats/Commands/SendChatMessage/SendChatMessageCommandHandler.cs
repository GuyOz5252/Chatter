using Chatter.Domain.Interfaces;
using Chatter.Domain.Specifications;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Application.Chats.Commands.SendChatMessage;

public class SendChatMessageCommandHandler(IChatRepository chatRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<SendChatMessageCommand>
{
    private readonly IChatRepository _chatRepository = chatRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(SendChatMessageCommand command, CancellationToken cancellationToken = default)
    {
        var chatResult = await _chatRepository
            .GetBySpecificationAsync(new ChatByIdSpecification(command.ChatId), cancellationToken);
        if (chatResult.IsFailure)
        {
            return chatResult;
        }
        var chat = chatResult.Value;

        var userResult = await _userRepository
            .GetBySpecificationAsync(new UserByIdSpecification(command.UserId), cancellationToken);
        if (userResult.IsFailure)
        {
            return userResult;
        }
        var user = userResult.Value;
        
        var specification = new UserIsChatParticipantSpecification(chat.Id, user.Id);
        if (!specification.Apply(chat))
        {
            return Result.Failure(Error.Forbidden("User is not in chat"));
        }
        
        chat.AddMessage(user, command.ChatMessageContent);
        
        var updateResult = await _chatRepository.UpdateAsync(chat, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return updateResult.IsFailure ? updateResult : Result.Success();
    }
}
