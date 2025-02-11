using SharedKernel.Interfaces;

namespace Application.Users.Commands.AddFriend;

public record AddFriendCommand(Guid UserId, Guid FriendToAddId) : ICommand;