using SharedKernel.Interfaces;
using Domain.Interfaces;
using SharedKernel.Results;

namespace Domain.Models;

public class User(Guid userId, string userName) : IAggregateRoot
{
    private readonly List<User> _friends = [];
    
    public Guid UserId { get; init; } = userId;
    
    public string UserName { get; private set; } = userName;
    
    public IReadOnlyCollection<User> Friends => _friends.AsReadOnly();

    public Result AddFriend(User friend)
    {
        if (_friends.Contains(friend))
        {
            return Error.Conflict(nameof(friend));
        }
        
        _friends.Add(friend);
        
        return Result.Success();
    }

    public Result RemoveFriend(User friend)
    {
        if (!_friends.Contains(friend))
        {
            return Error.NotFound(nameof(friend));
        }
        
        _friends.Remove(friend);
        
        return Result.Success();
    }
}