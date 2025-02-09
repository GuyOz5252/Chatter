using SharedKernel.Interfaces;

namespace Application.User.AddFriend;

public record AddFriendCommand(Guid UserId, Guid FriendToAddId) : ICommand;