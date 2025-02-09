using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Application.User.AddFriend;

public class AddFriendCommandHandler : ICommandHandler<AddFriendCommand>
{
    public Task<Result> HandleAsync(AddFriendCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}