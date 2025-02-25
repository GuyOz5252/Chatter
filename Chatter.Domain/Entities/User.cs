using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Domain.Entities;

public class User(Guid userId, string userName, string email) : IAggregateRoot
{
    private readonly List<Guid> _friendsIds = [];
    
    public Guid UserId { get; init; } = userId;
    
    public string UserName { get; private set; } = userName;

    public string Email { get; private set; } = email;
    
    public IReadOnlyCollection<Guid> FriendsIds => _friendsIds.AsReadOnly();

    public Result AddFriend(User friend)
    {
        if (_friendsIds.Contains(friend.UserId))
        {
            return Error.Conflict(nameof(friend));
        }
        
        _friendsIds.Add(friend.UserId);
        
        return Result.Success();
    }

    public Result RemoveFriend(User friend)
    {
        if (!_friendsIds.Contains(friend.UserId))
        {
            return Error.NotFound(nameof(friend));
        }
        
        _friendsIds.Remove(friend.UserId);
        
        return Result.Success();
    }
}