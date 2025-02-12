using SharedKernel.Interfaces;

namespace Chatter.Application.Users.Queries.List;

public record ListUsersQuery : IQuery<List<Domain.Entities.User>>;