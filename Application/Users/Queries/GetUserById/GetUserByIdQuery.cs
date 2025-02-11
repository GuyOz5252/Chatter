using SharedKernel.Interfaces;

namespace Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IQuery<Domain.Entities.User>;