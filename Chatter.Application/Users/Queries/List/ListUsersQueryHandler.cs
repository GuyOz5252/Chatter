using Chatter.Domain.Entities;
using Chatter.Domain.Interfaces;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Application.Users.Queries.List;

public class ListUsersQueryHandler(IUserRepository userRepository)
    : IQueryHandler<ListUsersQuery, List<User>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<List<User>>> HandleAsync(ListUsersQuery query, CancellationToken token = default)
    {
        var usersResult = await _userRepository.ListAsync(token);
        return usersResult.Match<List<User>>(
            value => value,
            error => Result<List<User>>.Failure(error));
    }
}