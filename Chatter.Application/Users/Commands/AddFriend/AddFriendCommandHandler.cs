using System.Reflection.Metadata.Ecma335;
using Chatter.Domain.Interfaces;
using Chatter.Domain.Specifications;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Application.Users.Commands.AddFriend;

public class AddFriendCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : ICommandHandler<AddFriendCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(AddFriendCommand command, CancellationToken cancellationToken = default)
    {
        if (command.UserId.Equals(command.FriendToAddId))
        {
            return Error.BadRequest("Can't add yourself as a friend");
        }
        
        var userResult = await _userRepository.GetBySpecificationAsync(new UserByIdSpecification(command.UserId), cancellationToken);
        if (userResult.IsFailure)
        {
            return userResult.Error;
        }
        
        var friendResult = await _userRepository.GetBySpecificationAsync(new UserByIdSpecification(command.FriendToAddId), cancellationToken);
        if (friendResult.IsFailure)
        {
            return friendResult.Error;
        }
        
        var user = userResult.Value;
        var friend = friendResult.Value;
        
        var result = user.AddFriend(friend);
        
        await _userRepository.UpdateAsync(user, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return result;
    }
}