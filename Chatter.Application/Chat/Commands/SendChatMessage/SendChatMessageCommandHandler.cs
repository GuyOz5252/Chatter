using Chatter.Domain.Interfaces;
using Chatter.Domain.Specifications;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Application.Chat.Commands.SendChatMessage;

public class SendChatMessageCommandHandler(IChatRepository chatRepository, IUserRepository userRepository)
    : ICommandHandler<SendChatMessageCommand>
{
    private readonly IChatRepository _chatRepository = chatRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result> HandleAsync(SendChatMessageCommand command, CancellationToken cancellationToken = default)
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
        
        var specification = new UserIsChatParticipantSpecification(chat.ChatId, user.UserId);
        if (!specification.Apply(chat))
        {
            return Result.Failure(Error.Forbidden("User is not in chat"));
        }
        
        chat.AddMessage(user, command.ChatMessageContent);
        
        return Result.Success();
    }
}
