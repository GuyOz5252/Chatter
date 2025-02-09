using SharedKernel.Interfaces;
using Domain.Interfaces;

namespace Domain.Models;

public class User(Guid userId, string userName) : IAggregateRoot
{
    private readonly List<User> _friends = [];
    
    public Guid UserId { get; init; } = userId;
    
    public string UserName { get; private set; } = userName;
    
    public IReadOnlyCollection<User> Friends => _friends.AsReadOnly();

    public void AddFriend(User friend)
    {
        if (_friends.Contains(friend))
        {
            return;
        }
        
        _friends.Add(friend);
    }

    public void RemoveFriend(User friend)
    {
        if (!_friends.Contains(friend))
        {
            return;
        }
        
        _friends.Remove(friend);
    }
}