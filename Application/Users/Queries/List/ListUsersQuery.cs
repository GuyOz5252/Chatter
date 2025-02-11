using SharedKernel.Interfaces;

namespace Application.Users.Queries.List;

public record ListUsersQuery : IQuery<List<Domain.Entities.User>>;