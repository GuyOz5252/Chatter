using Chatter.Domain.Entities;
using Chatter.Domain.Interfaces;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Application.Chats.Commands.CreateChat;

public class CreateChatCommandHandler : ICommandHandler<CreateChatCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    
    private readonly IChatRepository _chatRepository;
    
    private readonly IUnitOfWork _unitOfWork;

    public CreateChatCommandHandler(IUserRepository userRepository, IChatRepository chatRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _chatRepository = chatRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateChatCommand command, CancellationToken cancellationToken)
    {
        var chat = new Chat();
        foreach (var participantId in command.ParticipantIds)
        {
            var userResult = await _userRepository.GetByPk(participantId, cancellationToken);
            
            if (userResult.IsFailure)
            {
                return userResult.Error;
            }
            
            chat.AddParticipant(userResult.Value);
        }
        
        var addResult = await _chatRepository.AddAsync(chat, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error;
        }
        
        return addResult.Value.Id;
    }
}