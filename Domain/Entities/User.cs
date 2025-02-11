using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Domain.Entities;

public class User(Guid userId, string userName, string email) : IAggregateRoot
{
    private readonly List<User> _friends = [];
    
    public Guid UserId { get; init; } = userId;
    
    public string UserName { get; private set; } = userName;

    public string Email { get; private set; } = email;
    
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