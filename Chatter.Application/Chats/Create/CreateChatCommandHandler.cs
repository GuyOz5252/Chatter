using Chatter.Domain.Abstract;
using Chatter.Domain.Entities;
using SharedKernel;
using SharedKernel.Messaging;
using SharedKernel.Results;

namespace Chatter.Application.Chats.Create;

public class CreateChatCommandHandler : ICommandHandler<CreateChatCommand, Guid>
{
    private readonly IChatRepository _chatRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateChatCommandHandler(IChatRepository chatRepository, IUnitOfWork unitOfWork)
    {
        _chatRepository = chatRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> HandleAsync(CreateChatCommand command, CancellationToken cancellationToken = default)
    {
        var chat = new Chat(command.ParticipantsUserIds);

        _chatRepository.Create(chat);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return chat.Id;
    }
}
