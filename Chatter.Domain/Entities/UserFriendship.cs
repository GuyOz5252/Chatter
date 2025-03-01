namespace Chatter.Domain.Entities;

public class UserFriendship
{
    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public Guid FriendId { get; private set; }
    public User Friend { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
#pragma warning disable CS8618
    private UserFriendship()
    {
    }
#pragma warning restore CS8618
    
    public UserFriendship(User user, User friend)
    {
        User = user;
        UserId = user.Id;
        Friend = friend;
        FriendId = friend.Id;
    }
}