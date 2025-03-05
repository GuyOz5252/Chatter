using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Domain.Entities;

public class User : IAggregateRoot
{
    private readonly List<UserFriendship> _friendships = [];
    
    public Guid Id { get; init; } = Guid.NewGuid();
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public IReadOnlyList<UserFriendship> Friendships => _friendships.AsReadOnly();

    public User(string userName, string email)
    {
        UserName = userName;
        Email = email;
    }
    
    public Result AddFriend(User friend)
    {
        if (_friendships.Any(userFriendship => userFriendship.FriendId.Equals(friend.Id)))
        {
            return Error.Conflict(nameof(friend));
        }

        if (Id.Equals(friend.Id))
        {
            return Error.BadRequest("Can't add yourself as a friend");
        }
        
        _friendships.Add(new UserFriendship(this, friend));
        
        return Result.Success();
    }

    public Result RemoveFriend(User friend)
    {
        var userFriendship = _friendships.FirstOrDefault(userFriendship => userFriendship.FriendId.Equals(friend.Id));
        if (userFriendship is null)
        {
            return Error.NotFound(nameof(friend));
        }
        
        _friendships.Remove(userFriendship);
        
        return Result.Success();
    }
}