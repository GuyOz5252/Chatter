using Chatter.Domain.Interfaces;
using Chatter.Domain.Specifications;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Application.Users.Commands.AddFriend;

public class AddFriendCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : ICommandHandler<AddFriendCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<Result> HandleAsync(AddFriendCommand command, CancellationToken cancellationToken = default)
    {
        var userResult = await _userRepository.GetBySpecificationAsync(new UserByIdSpecification(command.UserId), cancellationToken);
        if (userResult.IsFailure)
        {
            return userResult.Error;
        }
        
        var friendResult = await _userRepository.GetBySpecificationAsync(new UserByIdSpecification(command.UserId), cancellationToken);
        if (friendResult.IsFailure)
        {
            return friendResult.Error;
        }
        
        var user = userResult.Value;
        var friend = friendResult.Value;
        
        user.AddFriend(friend);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return Result.Success();
    }
}