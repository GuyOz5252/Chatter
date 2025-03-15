namespace Chatter.Domain.Entities;

public class UserFriendship
{
    public Guid UserId { get; init; }
    
    public User User { get; init; }
    
    public Guid FriendId { get; init; }
    
    public User Friend { get; init; }
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
#pragma warning disable CS8618
    private UserFriendship()
    {
    }
#pragma warning restore CS8618
    
    internal UserFriendship(User user, User friend)
    {
        User = user;
        UserId = user.Id;
        Friend = friend;
        FriendId = friend.Id;
    }
}